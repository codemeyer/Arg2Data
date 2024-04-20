namespace Arg2Data.Entities;

/// <summary>
/// Header before list of track sections.
/// </summary>
public class TrackSectionHeader
{
    /// <summary>
    /// Gets or sets the angle of the first track section.
    /// </summary>
    public ushort FirstSectionAngle { get; set; }

    /// <summary>
    /// Gets or sets the height of the first track section.
    /// </summary>
    public short FirstSectionHeight { get; set; }

    /// <summary>
    /// Gets or sets the track center X position.
    /// </summary>
    public short TrackCenterX { get; set; }

    /// <summary>
    /// Gets or sets the track center Y position
    /// </summary>
    public short TrackCenterY { get; set; }

    /// <summary>
    /// Gets or sets the track center Z position
    /// </summary>
    public short TrackCenterZ { get; set; }

    /// <summary>
    /// Gets or sets the start width of the track.
    /// </summary>
    public short StartWidth { get; set; }

    //public SurroundingArea SurroundingArea { get; set; }

    /// <summary>
    /// Gets or sets the start width of the left verge.
    /// </summary>
    public byte LeftVergeStartWidth { get; set; }

    /// <summary>
    /// Gets or sets the start width of the right verge.
    /// </summary>
    public byte RightVergeStartWidth { get; set; }

    /// <summary>
    /// Gets the kerb type, describing the number of colors it has.
    /// </summary>
    public KerbType KerbType { get; set; }

    /// <summary>
    /// Gets the kerb upper color of untextured kerbs. Is it actually used in GP2?
    /// </summary>
    public byte KerbUpperColor { get; set; }

    /// <summary>
    /// Gets the kerb lower color of untextured kerbs. Is it actually used in GP2?
    /// </summary>
    public byte KerbLowerColor { get; set; }

    /// <summary>
    /// Gets the alternate kerb upper color of untextured kerbs. Is it actually used in GP2?
    /// </summary>
    public byte KerbUpperColor2 { get; set; }

    /// <summary>
    /// Gets the alternate kerb lower color of untextured kerbs. Is it actually used in GP2?
    /// </summary>
    public byte KerbLowerColor2 { get; set; }

    /// <summary>
    /// Gets or sets which side of track that the pole position is located.
    /// </summary>
    public TrackSide PoleSide { get; set; }

    /// <summary>
    /// Gets or sets which side of the track that the pit lane is located.
    /// </summary>
    public TrackSide PitsSide { get; set; }

    internal int CommandLength0xC5 { get; set; }

    internal int GetHeaderLength()
    {
            return KerbType == KerbType.DualColor ? 28 : 32;
        }
}

/// <summary>
/// Type of kerb, either two colors or three colors.
/// </summary>
public enum KerbType
{
    /// <summary>
    /// Two-colored kerb.
    /// </summary>
    DualColor,

    /// <summary>
    /// Three-colored kerb.
    /// </summary>
    TripleColor
}

/// <summary>
/// Track side.
/// </summary>
public enum TrackSide
{
    /// <summary>
    /// Left side of track.
    /// </summary>
    Left,

    /// <summary>
    /// Right side of track.
    /// </summary>
    Right
}