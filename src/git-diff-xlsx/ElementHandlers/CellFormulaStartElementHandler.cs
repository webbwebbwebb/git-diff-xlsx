using DocumentFormat.OpenXml;

namespace git_diff_xlsx.ElementHandlers
{
    public class CellFormulaStartElementHandler : StartElementHandler
    {
        public override void Invoke(OpenXmlReader reader, ref CellContext cellContext)
        {
            cellContext.Formula = reader.GetText();
        }
    }
}