using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class ObjectShapesReader
    {
        public static List<TrackObjectShape> Read(BinaryReader reader, int trackOffset)
        {
            reader.BaseStream.Position = 4124;

            short count = reader.ReadInt16();

            reader.BaseStream.Position += 2;

            var offsets = new List<int>();

            // list of offsets
            for (int i = 0; i < count; i++)
            {
                int offset = reader.ReadInt32();
                offsets.Add(offset);
            }

            var objects = new List<TrackObjectShape>();

            // huh?
            var startPos = 4128;

            var sortedOffsets = new List<int>();
            foreach (var offset in offsets)
            {
                sortedOffsets.Add(offset);
            }

            sortedOffsets.Sort();

            // foreach offset, read data between offset location and next offset location
            for (int headerIndex = 0; headerIndex < offsets.Count; headerIndex++)
            {
                var loopOffset = offsets[headerIndex];

                int nextOffset;

                if (offsets.Any(of => of > loopOffset))
                {
                    nextOffset = offsets.Where(of => of > loopOffset).ToList().Min();
                }
                else
                {
                    nextOffset = trackOffset - startPos;
                }

                var readFrom = startPos + loopOffset;
                var lengthToRead = nextOffset - loopOffset;

                reader.BaseStream.Position = readFrom;
                byte[] data = reader.ReadBytes(lengthToRead);

                var dataIndex = sortedOffsets.IndexOf(loopOffset);

                var shape = new TrackObjectShape(headerIndex, dataIndex);
                shape.AllData = data;

                //UpdateDataProperties(obj, data, readFrom);

                objects.Add(shape);
            }

            return objects;
        }
    }
}
