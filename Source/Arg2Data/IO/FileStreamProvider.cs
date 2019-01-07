using System.IO;

namespace Arg2Data.IO
{
    internal static class FileStreamProvider
    {
        public static Stream Open(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
