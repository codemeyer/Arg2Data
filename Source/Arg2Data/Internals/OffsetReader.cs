using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class OffsetReader
    {
        public static TrackOffsets Read(BinaryReader reader)
        {
            reader.BaseStream.Position = 4096;

            return new TrackOffsets
            {
                UnknownLong1 = reader.ReadInt32(),
                UnknownLong2 = reader.ReadInt32(),
                ChecksumPosition = reader.ReadInt32() + 4128,
                ObjectData = reader.ReadInt32() + 4128,
                TrackData = reader.ReadInt32() + 4128,
                ComputerCarSetup = reader.ReadInt32() + 4128,
                PitLaneData = reader.ReadInt32() + 4128
            };
        }
    }
}
