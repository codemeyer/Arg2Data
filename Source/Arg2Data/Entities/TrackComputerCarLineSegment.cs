namespace Arg2Data.Entities
{
    /// <summary>
    /// Describes a segment of the best line/computer car line.
    /// </summary>
    public class TrackComputerCarLineSegment
    {
        /// <summary>
        /// Gets the length of the segment.
        /// </summary>
        public byte Length { get; set; }

        /// <summary>
        /// Gets the type of segment.
        /// </summary>
        public byte Command { get; set; }

        /// <summary>
        /// Gets the correction value. Called Tighter/Wider in GP2 Track Editor.
        /// </summary>
        public ushort Correction { get; set; }

        /// <summary>
        /// Gets the sign/scale value.
        /// </summary>
        public byte SignScale { get; set; }

        /// <summary>
        /// Gets the corner radius.
        /// </summary>
        public ushort Radius { get; set; }

        /// <summary>
        /// Gets the sign for a 0x70 command segment.
        /// </summary>
        public byte Sign0x70 { get; set; }

        /// <summary>
        /// Gets the radius for a 0x70 command segment.
        /// </summary>
        public ushort Radius0x70 { get; set; }
    }
}
