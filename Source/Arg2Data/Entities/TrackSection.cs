namespace Arg2Data.Entities;

/// <summary>
/// Represents a small section of a track.
/// </summary>
public class TrackSection
{
    /// <summary>
    /// Initializes a new instance of a TrackSection.
    /// </summary>
    public TrackSection()
    {
            Commands = new List<TrackSectionCommand>();
        }

    /// <summary>
    /// Gets or sets the length of the section. 1 unit is 16 feet (approx 4.87 meters).
    /// </summary>
    public byte Length { get; set; }

    /// <summary>
    /// Gets the list of commands that describe, modify or enhance the track section.
    /// </summary>
    public List<TrackSectionCommand> Commands { get; set; }

    /// <summary>
    /// Gets or sets the curvature of the track. Positive numbers means a right turn, negative numbers means a left turn. Smaller numbers indicate a tighter turn.
    /// </summary>
    public short Curvature { get; set; }

    /// <summary>
    /// Gets or sets the height change that occurs in the section.
    /// </summary>
    public short Height { get; set; }

    /// <summary>
    /// Gets or sets the width of the left verge.
    /// </summary>
    public byte LeftVergeWidth { get; set; }

    /// <summary>
    /// Gets or sets the width of the right verge.
    /// </summary>
    public byte RightVergeWidth { get; set; }

    private TrackSectionFlags InternalFlags { get; set; }

    internal short Flags
    {
        set
        {
                InternalFlags = (TrackSectionFlags)Enum.Parse(typeof(TrackSectionFlags), value.ToString());
            }
    }

    /// <summary>
    /// Get whether the PitLaneEntrance flag is set for the section.
    /// </summary>
    public bool PitLaneEntrance
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.PitLaneEntrance); }
    }

    /// <summary>
    /// Get whether the PitLaneExit flag is set for the section.
    /// </summary>
    public bool PitLaneExit
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.PitLaneExit); }
    }

    /// <summary>
    /// Gets or sets the height of the kerb in the section, if there is one set with the HasLeftKerb or HasRightKerb flags.
    /// </summary>
    public KerbHeight KerbHeight
    {
        get
        {
                return InternalFlags.HasFlag(TrackSectionFlags.KerbHeight)
                    ? KerbHeight.Low
                    : KerbHeight.High;
            }
    }

    /// <summary>
    /// Gets whether there is a kerb on the left side of the track in the section.
    /// </summary>
    public bool HasLeftKerb
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.LeftKerb); }
    }

    /// <summary>
    /// Gets whether there is a kerb on the right side of the track in the section.
    /// </summary>
    public bool HasRightKerb
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.RightKerb); }
    }

    /// <summary>
    /// Gets whether 300/200/100 signs should appear before the section.
    /// </summary>
    public bool RoadSigns
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.RoadSigns); }
    }

    /// <summary>
    /// Gets or sets whether an arrow sign should appear before the section.
    /// </summary>
    public bool RoadSignArrow
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.RoadSignArrow); }
    }

    /// <summary>
    /// Gets whether an arrow and 100 sign should appear before the section.
    /// </summary>
    public bool RoadSignArrow100
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.RoadSignArrow100); }
    }

    /// <summary>
    /// Gets whether the right fence should be bridged with the fence of the previous?next? section.
    /// </summary>
    public bool BridgedRightFence
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.BridgedRightFence); }
    }

    /// <summary>
    /// Gets whether the left fence should be bridged with the fence of the previous?next? section.
    /// </summary>
    public bool BridgedLeftFence
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.BridgedLeftFence); }
    }

    /// <summary>
    /// Gets whether the right wall of the section should be removed/invisible.
    /// </summary>
    public bool RemoveRightWall
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.RightWallRemove); }
    }

    /// <summary>
    /// Gets whether the right wall of the section should be removed/invisible.
    /// </summary>
    public bool RemoveLeftWall
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.LeftWallRemove); }
    }

    /// <summary>
    /// Gets whether the Unknown1 flag (0x100) is set. Not used in any default track, probably has no use.
    /// </summary>
    public bool Unknown1
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.Unknown1); }
    }

    /// <summary>
    /// Gets whether the Unknown2 flag (0x200) is set. Not used in any default track, probably has no use.
    /// </summary>
    public bool Unknown2
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.Unknown2); }
    }

    /// <summary>
    /// Gets whether the Unknown3 flag (0x4000) is set.
    /// </summary>
    public bool Unknown3
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.Unknown3); }
    }

    /// <summary>
    /// Gets whether the Unknown4 flag (0x8000) is set. Not used in any default track, probably has no use.
    /// </summary>
    public bool Unknown4
    {
        get { return InternalFlags.HasFlag(TrackSectionFlags.Unknown4); }
    }

    [Flags]
    private enum TrackSectionFlags
    {
        PitLaneEntrance = 0x1,
        PitLaneExit = 0x2,
        KerbHeight = 0x4,
        RoadSigns = 0x8,
        BridgedRightFence = 0x10,
        BridgedLeftFence = 0x20,
        RoadSignArrow = 0x40,
        RoadSignArrow100 = 0x80,
        Unknown1 = 0x100,
        Unknown2 = 0x200,
        RightKerb = 0x400,
        LeftKerb = 0x800,
        RightWallRemove = 0x1000,
        LeftWallRemove = 0x2000,
        Unknown3 = 0x4000,
        Unknown4 = 0x8000
    }
}

/// <summary>
/// Defines the type of kerb that exists in the track section.
/// </summary>
public enum KerbHeight
{
    /// <summary>
    /// Low kerb, can be driven over.
    /// </summary>
    Low,

    /// <summary>
    /// High kerb, upsets the car when driven over.
    /// </summary>
    High
}
