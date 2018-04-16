namespace Arg2Data.Entities
{
    /// <summary>
    /// Offset positions of data in the track file.
    /// </summary>
    public class TrackOffsets
    {
        /// <summary>
        /// Gets the UnknownLong1 offset value.
        /// </summary>
        public int UnknownLong1 { get; internal set; }

        /// <summary>
        /// Gets the UnknownLong2 offset value.
        /// </summary>
        public int UnknownLong2 { get; internal set; }

        /// <summary>
        /// Gets the offset position of the file checksum.
        /// </summary>
        public int ChecksumPosition { get; internal set; }

        /// <summary>
        /// Gets the offset position of object data.
        /// </summary>
        public int ObjectData { get; internal set; }

        /// <summary>
        /// Gets the offset position of track section header and data.
        /// </summary>
        public int TrackData { get; internal set; }

        /// <summary>
        /// Gets the offset position of the computer car setup data.
        /// </summary>
        public int ComputerCarSetup { get; internal set; }

        /// <summary>
        /// Gets the offset position of pit lane section data.
        /// </summary>
        public int PitLaneData { get; internal set; }
    }
}
