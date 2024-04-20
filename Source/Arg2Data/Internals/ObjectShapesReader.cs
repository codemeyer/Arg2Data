using Arg2Data.Entities;

namespace Arg2Data.Internals;

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

            UpdateDataProperties(shape, data, readFrom);

            objects.Add(shape);
        }

        return objects;
    }

    private static void UpdateDataProperties(TrackObjectShape shapeData, byte[] data, int readFrom)
    {
        shapeData.Offset1 = BitConverter.ToInt16([data[4], data[5]], 0);
        shapeData.Offset2 = BitConverter.ToInt16([data[8], data[9]], 0);
        shapeData.Offset3 = BitConverter.ToInt16([data[12], data[13]], 0);
        shapeData.Offset4 = BitConverter.ToInt16([data[16], data[17]], 0);
        shapeData.Offset5 = BitConverter.ToInt16([data[20], data[21]], 0);
        shapeData.Offset6 = BitConverter.ToInt16([data[24], data[25]], 0);
        shapeData.Offset7 = BitConverter.ToInt16([data[28], data[29]], 0);

        int data0Length = shapeData.Offset2 - shapeData.Offset1;
        int data1Length = shapeData.Offset3 - shapeData.Offset2;
        int data2Length = shapeData.Offset6 - shapeData.Offset3;
        int data3Length = shapeData.Offset4 - shapeData.Offset6;
        int data4Length = shapeData.Offset5 - shapeData.Offset4;
        int data5Length = shapeData.Offset7 - shapeData.Offset5;

        int data0Start = 78;
        int data1Start = data0Start + data0Length;
        int data2Start = data1Start + data1Length;
        int data3Start = data2Start + data2Length;
        int data4Start = data3Start + data3Length;
        int data5Start = data4Start + data4Length;

        int pointsStart = data4Start;
        int pointsLength = data4Length;

        int vectorsStart = data5Start;
        int vectorsLength = data5Length;

        //1 15638    4
        //2 15642    4
        //3 15646   28
        //6 15674    8
        //4 15682   32  (points?)
        //5 15714   20  (vectors)
        //7 15734   96
        //  15830
        //  15830

        var scaleValues = GetScaleValues(data, data0Start);
        shapeData.ScaleValues = scaleValues;

        byte[] pointData = new byte[pointsLength];
        Array.Copy(data, pointsStart, pointData, 0, pointsLength);

        var points = GetPoints(pointData);
        shapeData.RawPoints.AddRange(points.Item1);
        //shapeData.PointsAdditionalBytes = points.Item2;
        shapeData.UpdatePoints();

        byte[] vectorData = new byte[vectorsLength];
        Array.Copy(data, vectorsStart, vectorData, 0, vectorsLength);

        var vectors = GetVectors(vectorData);
        shapeData.Vectors.AddRange(vectors);
    }

    /// <summary>
    /// Returns the list of scale values.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="scaleStart"></param>
    /// <returns></returns>
    private static List<short> GetScaleValues(byte[] data, int scaleStart)
    {
        var list = new List<short>();

        using var bin = new BinaryReader(new MemoryStream(data));
        bin.BaseStream.Position = scaleStart;

        while (true)
        {
            short scaleValue = bin.ReadInt16();

            if (scaleValue != 0)
            {
                list.Add(scaleValue);
            }
            else
            {
                break;
            }
        }

        return list;
    }

    /// <summary>
    /// Returns the list of points as well as an additional byte array that may contain any "stray" point bytes.
    /// </summary>
    /// <param name="rawData"></param>
    /// <returns></returns>
    private static Tuple<List<TrackObjectShapeRawPoint>, byte[]> GetPoints(byte[] rawData)
    {
        const int bytesPerEntry = 8;
        var list = new List<TrackObjectShapeRawPoint>();
        int steps = rawData.Length / bytesPerEntry;

        byte[] strayEndBytes = new byte[0];

        int strayByteCount = rawData.Length % bytesPerEntry;

        if (strayByteCount > 0)
        {
            strayEndBytes = new byte[strayByteCount];
            Array.Copy(rawData, rawData.Length - strayByteCount, strayEndBytes, 0, strayByteCount);
        }

        for (int i = 0; i < steps; i++)
        {
            int position = i * bytesPerEntry;

            var point = new TrackObjectShapeRawPoint
            {
                XCoord = BitConverter.ToInt16(rawData, position),
                ReferencePointValue = rawData[position],
                ReferencePointFlag = rawData[position + 1],
                YCoord = BitConverter.ToInt16(rawData, position + 2),
                ZCoord = BitConverter.ToUInt16(rawData, position + 4),
                Unknown = BitConverter.ToUInt16(rawData, position + 6)
            };

            list.Add(point);
        }

        return new Tuple<List<TrackObjectShapeRawPoint>, byte[]>(list, strayEndBytes);
    }

    private static List<TrackObjectShapeVector> GetVectors(byte[] rawData)
    {
        const int bytesPerEntry = 4;
        var list = new List<TrackObjectShapeVector>();
        int steps = rawData.Length / bytesPerEntry;

        for (int i = 0; i < steps; i++)
        {
            int position = i * bytesPerEntry;

            var vector = new TrackObjectShapeVector
            {
                From = rawData[position],
                To = rawData[position + 1]
            };

            list.Add(vector);
        }

        return list;
    }
}
