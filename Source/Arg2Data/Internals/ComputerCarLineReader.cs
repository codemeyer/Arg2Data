using System.Collections.Generic;
using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class ComputerCarLineReader
    {
        public static TrackComputerCarLineReadingResult Read(BinaryReader reader, int startPosition)
        {
            reader.BaseStream.Position = startPosition;

            var header = new TrackComputerCarLineHeader();
            header.LineStartX = reader.ReadByte();
            header.LineStartXHigh = reader.ReadByte();
            header.LineStartY = reader.ReadByte();
            reader.ReadByte();
            header.Unknown1 = reader.ReadUInt16();
            header.Unknown2 = reader.ReadUInt16();
            header.Unknown3 = reader.ReadUInt16();
            header.Unknown4 = reader.ReadUInt16();

            var segments = new List<TrackComputerCarLineSegment>();

            while (true)
            {
                byte byte1 = reader.ReadByte();
                byte byte2 = reader.ReadByte();

                if (byte1 == 0 && byte2 == 0)
                {
                    break;
                }

                var segment = new TrackComputerCarLineSegment();

                segment.Length = byte1;
                segment.Command = byte2;
                segment.Correction = reader.ReadUInt16();
                segment.SignScale = reader.ReadByte();
                reader.BaseStream.Position++;
                segment.Radius = reader.ReadUInt16();

                if (segment.Command == 0x50)
                {
                }
                if (segment.Command == 0x70)
                {
                    segment.Sign0x70 = reader.ReadByte();
                    reader.BaseStream.Position++;
                    segment.Radius0x70 = reader.ReadUInt16();
                }

                segments.Add(segment);
            }

            int position = (int)reader.BaseStream.Position;
            return new TrackComputerCarLineReadingResult(position, header, segments);
        }
    }
}
