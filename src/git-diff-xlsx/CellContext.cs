namespace git_diff_xlsx
{
    public class CellContext
    {
        public string Address { get; set; }
        public string Formula { get; set; }
        public string RawValue { get; set; }
        public int? StyleIndex { get; set; }

        public string GetValue()
        {
            switch (ValueType)
            {
                case CellValueTypeEnum.Boolean:
                    return RawValue == "1" ? "TRUE" : "FALSE";
                case CellValueTypeEnum.LookupString:
                case CellValueTypeEnum.InlineString:
                case CellValueTypeEnum.InlineValue:
                    return RawValue;
            }

            return null;
        }

        public CellValueTypeEnum ValueType { get; set; }

        public override string ToString()
        {
            return $"Address: {Address}, Value: {GetValue()}, Type: {ValueType}, Formula: {Formula}, RawValue: {RawValue}, StyleIndex: {StyleIndex}";
        }
    }
}
