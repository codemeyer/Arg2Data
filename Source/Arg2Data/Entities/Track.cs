using System.Collections.Generic;

namespace Arg2Data.Entities
{
    /// <summary>
    /// Represents a GP2 track.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Initializes a new instance of a track.
        /// </summary>
        public Track()
        {
            Offsets = new TrackOffsets();
            ObjectShapes = new List<TrackObjectShape>();
            ObjectSettings = new List<TrackObjectSettings>();
            TrackDataHeader = new TrackSectionHeader();
            TrackSections = new List<TrackSection>();
            ComputerCarLineSegments = new List<TrackComputerCarLineSegment>();
            ComputerCarSetup = new Setup();
            PitLaneSections = new List<TrackSection>();
            ComputerCarBehavior = new ComputerCarBehavior();
        }

        /// <summary>
        /// Data offset values.
        /// </summary>
        public TrackOffsets Offsets { get; internal set; }

        /// <summary>
        /// Gets the list of object shapes.
        /// </summary>
        public IList<TrackObjectShape> ObjectShapes { get; internal set; }

        /// <summary>
        /// Gets the list of object settings.
        /// </summary>
        public IList<TrackObjectSettings> ObjectSettings { get; internal set; }

        /// <summary>
        /// Gets the list of track sections.
        /// </summary>
        public IList<TrackSection> TrackSections { get; internal set; }

        /// <summary>
        /// Gets the track data header.
        /// </summary>
        public TrackSectionHeader TrackDataHeader { get; internal set; }

        /// <summary>
        /// Best line header.
        /// </summary>
        public TrackComputerCarLineHeader ComputerCarLineHeader { get; internal set; }

        /// <summary>
        /// List of best line segments.
        /// </summary>
        public IList<TrackComputerCarLineSegment> ComputerCarLineSegments { get; internal set; }

        /// <summary>
        /// Gets the computer car setup.
        /// </summary>
        public Setup ComputerCarSetup { get; set; }

        /// <summary>
        /// Gets the list of pit lane sections.
        /// </summary>
        public IList<TrackSection> PitLaneSections { get; internal set; }

        /// <summary>
        /// Gets the computer car behavior.
        /// </summary>
        public ComputerCarBehavior ComputerCarBehavior { get; set; }

        /// <summary>
        /// Gets the track settings.
        /// </summary>
        public TrackSettings TrackSettings { get; internal set; }
    }
}
