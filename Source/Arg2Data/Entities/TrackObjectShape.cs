namespace Arg2Data.Entities;

/// <summary>
/// Object 3D shape.
/// </summary>
public class TrackObjectShape
{
    /// <summary>
    /// Initializes a new instance of a TrackObjectShape.
    /// </summary>
    /// <param name="headerIndex"></param>
    /// <param name="dataIndex"></param>
    internal TrackObjectShape(int headerIndex, int dataIndex)
    {
        HeaderIndex = headerIndex;
        DataIndex = dataIndex;
        ScaleValues = new List<short>();
        RawPoints = new List<TrackObjectShapeRawPoint>();
        Points = new List<ITrackObjectShapePoint>();
        Vectors = new List<TrackObjectShapeVector>();
    }

    /// <summary>
    /// Gets the header index of the object shape.
    /// </summary>
    public int HeaderIndex { get; }

    /// <summary>
    /// Gets the data index of the object shape.
    /// </summary>
    public int DataIndex { get; }

    /// <summary>
    /// Gets all the raw object data.
    /// </summary>
    public byte[] AllData { get; internal set; }

    internal short Offset1 { get; set; }
    internal short Offset2 { get; set; }
    internal short Offset3 { get; set; }
    internal short Offset4 { get; set; }
    internal short Offset5 { get; set; }
    internal short Offset6 { get; set; }
    internal short Offset7 { get; set; }

    /// <summary>
    /// Gets the list of ScaleValues.
    /// </summary>
    public List<short> ScaleValues { get; set; }

    /// <summary>
    /// Gets the list of raw point data.
    /// </summary>
    internal List<TrackObjectShapeRawPoint> RawPoints { get; }

    /// <summary>
    /// Gets the list of Points in the 3D shape.
    /// </summary>
    public List<ITrackObjectShapePoint> Points { get; private set; }

    /// <summary>
    /// Uses the RawPoints to update the 3D points using scale values (ScaleValues) and the raw point data (RawPoints).
    /// </summary>
    public void UpdatePoints()
    {
        Points = [];

        foreach (var point in RawPoints)
        {
            if (point.ReferencePointFlag == 0)
            {
                var currentPoint = new TrackObjectShapeScalePoint(this);

                if (point.XCoord >= 132)
                {
                    short index = (short)((point.XCoord - 128 - 4) / 4);
                    currentPoint.XScaleValueIndex = index;
                    currentPoint.XIsNegative = true;
                }
                else if (point.XCoord != 0)
                {
                    short index = (short)((point.XCoord - 4) / 4);
                    currentPoint.XScaleValueIndex = index;
                }
                else
                {
                    currentPoint.XScaleValueIndex = -1;
                }

                if (point.YCoord >= 132)
                {
                    short index = (short)((point.YCoord - 128 - 4) / 4);
                    currentPoint.YScaleValueIndex = index;
                    currentPoint.YIsNegative = true;
                }
                else if (point.YCoord != 0)
                {
                    short index = (short)((point.YCoord - 4) / 4);
                    currentPoint.YScaleValueIndex = index;
                }
                else
                {
                    currentPoint.YScaleValueIndex = -1;
                }

                currentPoint.Z = point.ZCoord;

                Points.Add(currentPoint);
            }
            else if (point.ReferencePointFlag == 0x80)
            {
                var currentRefPoint = new TrackObjectShapeReferencePoint(this)
                {
                    XPointIndex = point.ReferencePointValue,
                    YPointIndex = point.ReferencePointValue,
                    Z = point.ZCoord
                };

                Points.Add(currentRefPoint);
            }
        }
    }

    /// <summary>
    /// Gets the list of Vectors.
    /// </summary>
    public List<TrackObjectShapeVector> Vectors { get; }
}
