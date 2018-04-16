using System.Collections.Generic;
using System.Linq;
using Arg2Data.Entities;
using Arg2Data.IO;

namespace Arg2Data.Internals
{
    internal static class ObjectShapesReader
    {
        public static List<TrackObjectShape> Read(string path, int trackOffset)
        {
            var trackFileReader = new FileReader(path);

            const short startPosition = 4124;
            int pos = startPosition;

            short count = trackFileReader.ReadInt16(pos);

            pos += 4;

            var offsets = new List<int>();

            // list of offsets
            for (int i = 0; i < count; i++)
            {
                int offset = trackFileReader.ReadInt32(pos);
                offsets.Add(offset);

                pos += 4;
            }

            var objects = new List<TrackObjectShape>();

            var sortedOffsets = new List<int>();
            foreach (var offset in offsets)
            {
                sortedOffsets.Add(offset);
            }
            sortedOffsets.Sort();

            var startPos = 4128;

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

                byte[] data = trackFileReader.ReadBytes(readFrom, lengthToRead);

                var dataIndex = sortedOffsets.IndexOf(loopOffset);

                var shape = new TrackObjectShape(headerIndex, dataIndex);
                shape.AllData = data;

                // TODO: put stuff in separate Data lumps

                objects.Add(shape);
            }

            return objects;
        }
    }
}
