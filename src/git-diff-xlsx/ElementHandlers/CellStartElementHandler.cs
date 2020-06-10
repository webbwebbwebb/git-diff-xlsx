using System.Linq;
using DocumentFormat.OpenXml;

namespace git_diff_xlsx.ElementHandlers
{
    public class CellStartElementHandler: StartElementHandler
    {
        public override void Invoke(OpenXmlReader reader, ref CellContext cellContext)
        {
            cellContext = new CellContext();

            var addressAttribute = reader.Attributes.FirstOrDefault(x => x.LocalName == "r");
            cellContext.Address = addressAttribute.Value;

            var typeAttribute = reader.Attributes.FirstOrDefault(x => x.LocalName == "t");
            switch (typeAttribute.Value)
            {
                case "b":
                    cellContext.ValueType = CellValueTypeEnum.Boolean;
                    break;
                case "s":
                    cellContext.ValueType = CellValueTypeEnum.LookupString;
                    break;
                case "str":
                    cellContext.ValueType = CellValueTypeEnum.InlineString;
                    break;
                default:
                    cellContext.ValueType = CellValueTypeEnum.InlineValue;
                    break;
            }

            var styleAttribute = reader.Attributes.FirstOrDefault(x => x.LocalName == "s");
            if(int.TryParse(styleAttribute.Value, out var styleIndex))
            {
                cellContext.StyleIndex = styleIndex;
            }
        }
    }
}