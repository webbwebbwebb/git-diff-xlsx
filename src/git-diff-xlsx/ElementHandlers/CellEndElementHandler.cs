using System.Collections.Generic;
using System.IO;

namespace git_diff_xlsx.ElementHandlers
{
    public class CellEndElementHandler : EndElementHandler
    {
        private readonly string[] _sharedStringTable;
        private readonly Dictionary<int, string> _numberingFormatsByStyleIndex;

        public CellEndElementHandler(string[] sharedStringTable,  Dictionary<int, string> numberingFormatsByStyleIndex)
        {
            _sharedStringTable = sharedStringTable;
            _numberingFormatsByStyleIndex = numberingFormatsByStyleIndex;
        }

        public override void Invoke(CellContext cellContext, TextWriter output)
        {
            if (cellContext != null)
            {
                output.WriteLine("    " + Format(cellContext));
            }
        }

        public string Format(CellContext cellContext)
        {
            var value = cellContext.GetValue();
            if (cellContext.ValueType == CellValueTypeEnum.LookupString)
            {
                value = _sharedStringTable[int.Parse(value)];
            }

            if(!string.IsNullOrEmpty(value) && cellContext.StyleIndex.HasValue && _numberingFormatsByStyleIndex.TryGetValue(cellContext.StyleIndex.Value, out var numberingFormat))
            {
                value = double.Parse(value).ToString(numberingFormat.Replace('_', ' '));
            }

            return string.IsNullOrEmpty(value) && string.IsNullOrEmpty(cellContext.Formula)
                ? null
                : $"{cellContext.Address}: {value} {cellContext.Formula}".Trim();
        }
    }
}
