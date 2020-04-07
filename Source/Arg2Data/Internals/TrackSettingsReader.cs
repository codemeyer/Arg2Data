using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class TrackSettingsReader
    {
        internal static TrackSettings Read(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position + 22;

            return new TrackSettings
            {
                LapCount = reader.ReadInt16()
            };
        }
    }
}
