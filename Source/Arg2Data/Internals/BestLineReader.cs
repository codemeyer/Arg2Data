using System.Collections.Generic;
using Arg2Data.Entities;
using Arg2Data.IO;

namespace Arg2Data.Internals
{
    internal static class BestLineReader
    {
        public static TrackBestLineReadResult Read(string path, int startPosition)
        {
            var trackFileReader = new FileReader(path);

            var header = new TrackComputerCarLineHeader();
            header.LineStartX = trackFileReader.ReadByte(startPosition);
            header.LineStartXHigh = trackFileReader.ReadByte(startPosition + 1);
            header.LineStartY = trackFileReader.ReadByte(startPosition + 2);
            header.Unknown1= trackFileReader.ReadUInt16(startPosition + 4);
            header.Unknown2 = trackFileReader.ReadUInt16(startPosition + 6);
            header.Unknown3 = trackFileReader.ReadUInt16(startPosition + 8);
            header.Unknown4 = trackFileReader.ReadUInt16(startPosition + 10);

            var segments = new List<TrackComputerCarLineSegment>();
            int position = startPosition + 12;

            while (true)
            {
                byte byte1 = trackFileReader.ReadByte(position);
                byte byte2 = trackFileReader.ReadByte(position + 1);

                if (byte1 == 0 && byte2 == 0)
                {
                    break;
                }

                var segment = new TrackComputerCarLineSegment();

                segment.Length = trackFileReader.ReadByte(position);
                segment.Command = trackFileReader.ReadByte(position + 1);
                segment.Correction = trackFileReader.ReadUInt16(position + 2);
                segment.SignScale = trackFileReader.ReadByte(position + 4);
                segment.Radius = trackFileReader.ReadUInt16(position + 6);

                if (segment.Command == 0x50)
                {
                    position += 8;
                }
                if (segment.Command == 0x70)
                {
                    segment.Sign0x70 = trackFileReader.ReadByte(position + 8);
                    segment.Radius0x70 = trackFileReader.ReadUInt16(position + 10);
                    position += 12;
                }

                segments.Add(segment);
            }

            return new TrackBestLineReadResult(position, header, segments);
        }
    }
}
