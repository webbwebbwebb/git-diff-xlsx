using System.IO;

namespace git_diff_xlsx.ElementHandlers
{
    public abstract class EndElementHandler
    {
        public abstract void Invoke(CellContext cellContext, TextWriter output);
    }
}