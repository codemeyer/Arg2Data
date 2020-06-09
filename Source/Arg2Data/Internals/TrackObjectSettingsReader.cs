using System.Collections.Generic;
using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class TrackObjectSettingsReader
    {
        internal static List<TrackObjectSettings> Read(BinaryReader reader, int offsetsObjectData, int trackDataOffset)
        {
            var list = new List<TrackObjectSettings>();
            var position = offsetsObjectData;
            short offset = 0;

            reader.BaseStream.Position = offsetsObjectData;

            while (position < trackDataOffset)
            {
                var trackObject = new TrackObjectSettings
                {
                    Id = reader.ReadByte(),
                    DetailLevel = reader.ReadByte(),
                    Unknown = reader.ReadInt16(),
                    DistanceFromTrack = reader.ReadInt16(),
                    AngleX = reader.ReadInt16(),
                    AngleY = reader.ReadInt16(),
                    Unknown2 = reader.ReadInt16(),
                    Height = reader.ReadInt16(),
                    Id2 = reader.ReadInt16(),
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
