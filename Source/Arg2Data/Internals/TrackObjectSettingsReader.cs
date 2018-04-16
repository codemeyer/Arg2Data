using System.Collections.Generic;
using Arg2Data.Entities;
using Arg2Data.IO;

namespace Arg2Data.Internals
{
    internal static class TrackObjectSettingsReader
    {
        internal static List<TrackObjectSettings> Read(string path, int offsetsObjectData, int trackDataOffset)
        {
            var trackFileReader = new FileReader(path);

            var list = new List<TrackObjectSettings>();
            var position = offsetsObjectData;
            short offset = 0;

            while (position < trackDataOffset)
            {
                var trackObject = new TrackObjectSettings
                {
                    Id = trackFileReader.ReadByte(position),
                    DetailLevel = trackFileReader.ReadByte(position + 1),
                    Unknown = trackFileReader.ReadInt16(position + 2),
                    DistanceFromTrack = trackFileReader.ReadInt16(position + 4),
                    AngleX = trackFileReader.ReadInt16(position + 6),
                    AngleY = trackFileReader.ReadInt16(position + 8),
                    Unknown2 = trackFileReader.ReadInt16(position + 10),
                    Height = trackFileReader.ReadInt16(position + 12),
                    Id2 = trackFileReader.ReadInt16(position + 14),
                    Offset = offset
                };

                position += 16;
                offset += 16;

                list.Add(trackObject);
            }

            return list;
        }
    }
}
