using System.Collections.Concurrent;
using System.IO;

namespace Arg2Data.Tests
{
    internal static class MemoryStreamProvider
    {
        private static readonly ConcurrentDictionary<string, byte[]> FilesAndBytes = new ConcurrentDictionary<string, byte[]>();

        public static Stream Open(string path)
        {
            var bytes = FilesAndBytes.GetOrAdd(path, File.ReadAllBytes(path));

            return new MemoryStream(bytes);
        }
    }
}
