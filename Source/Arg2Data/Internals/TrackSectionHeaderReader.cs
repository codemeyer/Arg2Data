using Arg2Data.Entities;
using Arg2Data.IO;

namespace Arg2Data.Internals
{
    internal static class TrackSectionHeaderReader
    {
        public static TrackSectionHeader Read(string path, int offset)
        {
            var trackFileReader = new FileReader(path);

            byte kerbTypeByte = trackFileReader.ReadByte(offset + 18);
            short pitsValue = trackFileReader.ReadInt16(offset + 14);
            
            var header = new TrackSectionHeader
            {
                FirstSectionAngle = trackFileReader.ReadUInt16(offset),
                FirstSectionHeight = trackFileReader.ReadInt16(offset + 2),
                // is this order really different, or it TrackEditor mixing stuff up?
                TrackCenterX = trackFileReader.ReadInt16(offset + 8),
                TrackCenterZ = trackFileReader.ReadInt16(offset + 6),
                TrackCenterY = trackFileReader.ReadInt16(offset + 4),
                StartWidth = trackFileReader.ReadInt16(offset + 10),
                PoleSide = trackFileReader.ReadInt16(offset + 12) == -768 ? TrackSide.Left : TrackSide.Right,
                PitsSide = GetPitsSide(pitsValue),

                CommandLength0xC5 = GetCommandLength(pitsValue),

                RightVergeStartWidth = trackFileReader.ReadByte(offset + 16),
                LeftVergeStartWidth = trackFileReader.ReadByte(offset + 17),
                KerbType = GetKerbType(kerbTypeByte),
                KerbUpperColor = trackFileReader.ReadByte(offset + 22),
                KerbLowerColor = trackFileReader.ReadByte(offset + 24)
            };

            if (header.KerbType == KerbType.TripleColor)
            {
                header.KerbUpperColor2 = trackFileReader.ReadByte(offset + 28);
                header.KerbLowerColor2 = trackFileReader.ReadByte(offset + 30);
            }

            return header;
        }

        private static TrackSide GetPitsSide(short pitsValue)
        {
            int isSet2 = pitsValue & 2;
            int isSet8 = pitsValue & 8;

            return (isSet2 + isSet8 == 10) ? TrackSide.Left : TrackSide.Right;
        }

        private static int GetCommandLength(short pitsValue)
        {
            int isSet = pitsValue & 512;
            return isSet == 0 ? 7 : 8;
        }

        private static KerbType GetKerbType(byte kerbTypeByte)
        {
            return kerbTypeByte == 4 ? KerbType.TripleColor : KerbType.DualColor;
        }
    }
}
