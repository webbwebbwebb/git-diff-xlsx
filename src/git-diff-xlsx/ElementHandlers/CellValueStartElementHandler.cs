using DocumentFormat.OpenXml;

namespace git_diff_xlsx.ElementHandlers
{
    public class CellValueStartElementHandler : StartElementHandler
    {
        public override void Invoke(OpenXmlReader reader, ref CellContext cellContext)
        {
            cellContext.RawValue = reader.GetText();
        }
    }
}