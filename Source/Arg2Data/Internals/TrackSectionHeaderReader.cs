using Arg2Data.Entities;

namespace Arg2Data.Internals;

internal static class TrackSectionHeaderReader
{
    public static TrackSectionHeader Read(BinaryReader reader, int offset)
    {
        reader.BaseStream.Position = offset + 14;
        short pitsValue = reader.ReadInt16();

        reader.BaseStream.Position = offset + 18;
        byte kerbTypeByte = reader.ReadByte();

        reader.BaseStream.Position = offset;

        var header = new TrackSectionHeader
        {
            FirstSectionAngle = reader.ReadUInt16(),
            FirstSectionHeight = reader.ReadInt16(),
            TrackCenterY = reader.ReadInt16(),
            TrackCenterZ = reader.ReadInt16(),
            TrackCenterX = reader.ReadInt16(),
            StartWidth = reader.ReadInt16(),
            PoleSide = GetPoleSide(reader.ReadInt16()),
            PitsSide = GetPitsSide(reader.ReadInt16()),
            RightVergeStartWidth = reader.ReadByte(),
            LeftVergeStartWidth = reader.ReadByte()
        };

        header.CommandLength0xC5 = GetCommandLength(pitsValue);

        header.KerbType = GetKerbType(kerbTypeByte);
        reader.BaseStream.Position = offset + 22;
        header.KerbUpperColor = reader.ReadByte();
        reader.BaseStream.Position = offset + 24;
        header.KerbLowerColor = reader.ReadByte();

        if (header.KerbType == KerbType.TripleColor)
        {
            reader.BaseStream.Position = offset + 28;
            header.KerbUpperColor2 = reader.ReadByte();
            reader.BaseStream.Position = offset + 30;
            header.KerbLowerColor2 = reader.ReadByte();
        }

        return header;
    }

    private static TrackSide GetPoleSide(short poleSide)
    {
        return poleSide == -768 ? TrackSide.Left : TrackSide.Right;
    }

    private static TrackSide GetPitsSide(short pitsValue)
    {
        int isSet2 = pitsValue & 2;
        int isSet8 = pitsValue & 8;

        return isSet2 + isSet8 == 10 ? TrackSide.Left : TrackSide.Right;
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
