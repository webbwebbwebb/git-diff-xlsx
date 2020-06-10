using DocumentFormat.OpenXml;

namespace git_diff_xlsx.ElementHandlers
{
    public class CellInlineStringStartElementHandler : StartElementHandler
    {
        public override void Invoke(OpenXmlReader reader, ref CellContext cellContext)
        {
            cellContext.RawValue = reader.GetText();
        }
    }
}