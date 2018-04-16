using Arg2Data.Entities;
using Arg2Data.IO;

namespace Arg2Data.Internals
{
    internal static class OffsetReader
    {
        public static TrackOffsets Read(string path)
        {
            var trackFileReader = new FileReader(path);

            return new TrackOffsets
            {
                UnknownLong1 = trackFileReader.ReadInt32(4096),
                UnknownLong2 = trackFileReader.ReadInt32(4100),
                ChecksumPosition = trackFileReader.ReadInt16(4104) + 4128,
                ObjectData = trackFileReader.ReadInt32(4108) + 4128,
                TrackData = trackFileReader.ReadInt32(4112) + 4128,
                ComputerCarSetup = trackFileReader.ReadInt32(0x1014) + 4128,
                PitLaneData = trackFileReader.ReadInt32(0x1018) + 4128
            };
        }
    }
}
