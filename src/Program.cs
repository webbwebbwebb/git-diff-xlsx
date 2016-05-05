using System;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace git_diff_xlsx
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("usage: git-diff-xlsx.exe infile.xlsx");
                return -1;
            }

            Parse(args[0], Console.Out);
            return 0;
        }

        static void Parse(string inputFilePath, TextWriter output)
        {
            var inputFile = new FileInfo(inputFilePath);
            var package = new ExcelPackage(inputFile);

            PrintNames(package.Workbook, output);

            PrintLastEditedBy(package.Workbook, output);

            foreach (var sheet in package.Workbook.Worksheets.OrderBy(x => x.Index))
            {
                PrintSheetContent(sheet, output);
            }
        }

        static void PrintNames(ExcelWorkbook workbook, TextWriter output)
        {
            output.WriteLine(string.Join(",", workbook.Worksheets.OrderBy(x => x.Index).Select(x => x.Name)));
        }

        static void PrintLastEditedBy(ExcelWorkbook workbook, TextWriter output)
        {
            output.WriteLine("File last edited by " + workbook.Properties.LastModifiedBy);
        }

        static void PrintSheetContent(ExcelWorksheet sheet, TextWriter output)
        {
            output.WriteLine("=================================");
            output.WriteLine("Sheet: " + sheet.Name + "[ " + sheet.RowCount() + " , " + sheet.ColumnCount() + " ]");
            output.WriteLine("=================================");

            for (int row = 1; row < sheet.RowCount() + 1; row++)
            {
                for (int column = 1; column < sheet.ColumnCount() + 1; column++)
                {
                    var cell = sheet.Cells[row, column];
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        output.WriteLine("    " + cell.Address + ": " + cell.Text);
                    }
                }
            }
            output.WriteLine();
        }
    }
}
