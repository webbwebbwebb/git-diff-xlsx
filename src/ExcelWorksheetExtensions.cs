using OfficeOpenXml;

namespace git_diff_xlsx
{
    public static class ExcelWorksheetExtensions
    {
        public static int ColumnCount(this ExcelWorksheet worksheet)
        {
            if (worksheet == null || worksheet.Dimension == null || worksheet.Dimension.End == null)
            {
                return 0;
            }
            return worksheet.Dimension.End.Column;
        }

        public static int RowCount(this ExcelWorksheet worksheet)
        {
            if (worksheet == null || worksheet.Dimension == null || worksheet.Dimension.End == null)
            {
                return 0;
            }
            return worksheet.Dimension.End.Row;
        }
    }
}