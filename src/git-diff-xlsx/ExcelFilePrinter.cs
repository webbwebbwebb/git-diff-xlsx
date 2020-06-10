using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using git_diff_xlsx.ElementHandlers;

namespace git_diff_xlsx
{
    public class ExcelFilePrinter
    {
        public void Print(Stream input, TextWriter output)
        {
            using (var document = SpreadsheetDocument.Open(input, false) )
            {
                var sheetNames = document.WorkbookPart.Workbook.Sheets
                    .Descendants<Sheet>()
                    .ToDictionary(k => k.Id.Value, v => v.Name.Value);

                PrintSheetNames(sheetNames, output);

                PrintLastEditedBy(document.PackageProperties, output);

                string[] sharedStringTable = document.WorkbookPart.SharedStringTablePart.SharedStringTable
                    .Elements<SharedStringItem>()
                    .Select(x => x.InnerText)
                    .ToArray();

                var numberingFormatsByStyleIndex = CompileNumberingFormatsByStyleIndex(document.WorkbookPart.WorkbookStylesPart.Stylesheet);

                foreach(var worksheetPart in document.WorkbookPart.WorksheetParts)
                {
                    var worksheetName = sheetNames[document.WorkbookPart.GetIdOfPart(worksheetPart)];
                    PrintSheetContent(worksheetPart, worksheetName, sharedStringTable, numberingFormatsByStyleIndex, output);
                }
            }
        }

        private Dictionary<int, string> CompileNumberingFormatsByStyleIndex(Stylesheet stylesheet)
        {
            var numberingFormatsByStyleIndex = new Dictionary<int, string>();
            if (stylesheet.NumberingFormats != null)
            {
                var numberingFormats = stylesheet.NumberingFormats
                    .Elements<NumberingFormat>()
                    .ToDictionary(k => k.NumberFormatId.Value, v => v.FormatCode.Value);

                var cellFormats = stylesheet.CellFormats.Descendants<CellFormat>().ToArray();
                for (int i = 0; i < cellFormats.Length; i++)
                {
                    var cellFormat = cellFormats[i];
                    if (cellFormat.ApplyNumberFormat != null && cellFormat.ApplyNumberFormat.HasValue && cellFormat.ApplyNumberFormat.Value)
                    {
                        if (numberingFormats.TryGetValue(cellFormat.NumberFormatId.Value, out var numberingFormat))
                        {
                            numberingFormatsByStyleIndex.Add(i, numberingFormat);
                        }
                    }
                }
            }

            return numberingFormatsByStyleIndex;
        }

        private void PrintSheetNames(Dictionary<string, string> sheetNames, TextWriter output)
        {
            output.WriteLine(string.Join(",", sheetNames.Select(x => x.Value)));
        }

        private void PrintLastEditedBy(PackageProperties documentProperties, TextWriter output)
        {
            output.WriteLine("File last edited by " + documentProperties.LastModifiedBy);
        }

        private void PrintSheetContent(WorksheetPart worksheetPart, string worksheetName, string[] sharedStringTable, Dictionary<int, string> numberingFormatsByStyleIndex, TextWriter output)
        {
            output.WriteLine("=================================");
            output.WriteLine($"Sheet: {worksheetName} [ {worksheetPart.Worksheet.SheetDimension.Reference.Value} ]");
            output.WriteLine("=================================");

            var startElementHandlers = new Dictionary<Type, StartElementHandler>
            {
                {typeof(Cell), new CellStartElementHandler()},
                {typeof(CellFormula), new CellFormulaStartElementHandler()},
                {typeof(CellValue), new CellValueStartElementHandler()},
                {typeof(InlineString), new CellInlineStringStartElementHandler()}
            };

            var endElementHandlers = new Dictionary<Type, EndElementHandler>
            {
                {typeof(Cell), new CellEndElementHandler(sharedStringTable, numberingFormatsByStyleIndex)}
            };

            OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
            CellContext cellContext = null;
            while (reader.Read())
            {
                var elementType = reader.ElementType; 
                if (reader.IsStartElement)
                {
                    if(startElementHandlers.TryGetValue(elementType, out var startElementHandler))
                    {
                        startElementHandler.Invoke(reader, ref cellContext);
                    }
                }
                
                if (reader.IsEndElement)
                {
                    if (endElementHandlers.TryGetValue(elementType, out var endElementHandler))
                    {
                        endElementHandler.Invoke(cellContext, output);
                    }
                }
            }
        }
    }
}