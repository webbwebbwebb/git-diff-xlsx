using DocumentFormat.OpenXml;

namespace git_diff_xlsx.ElementHandlers
{
    public abstract class StartElementHandler
    {
        public abstract void Invoke(OpenXmlReader reader, ref CellContext cellContext);
    }
}