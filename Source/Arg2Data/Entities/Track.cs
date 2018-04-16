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
            //RawData = new TrackRawData();
            ObjectShapes = new List<TrackObjectShape>();
            ObjectSettings = new List<TrackObjectSettings>();
            TrackDataHeader = new TrackSectionHeader();
            TrackSections = new List<TrackSection>();
            BestLineSegments = new List<TrackBestLineSegment>();
            ComputerCarSetup = new Setup();
            PitLaneSections = new List<TrackSection>();
        }

        /// <summary>
        /// Data offset values.
        /// </summary>
        public TrackOffsets Offsets { get; internal set; }

        /// <summary>
        /// Object 3D shapes.
        /// </summary>
        public IList<TrackObjectShape> ObjectShapes { get; internal set; }

        /// <summary>
        /// List of object settings.
        /// </summary>
        public IList<TrackObjectSettings> ObjectSettings { get; internal set; }

        /// <summary>
        /// List of track sections.
        /// </summary>
        public IList<TrackSection> TrackSections { get; internal set; }

        /// <summary>
        /// Track data header.
        /// </summary>
        public TrackSectionHeader TrackDataHeader { get; internal set; }

        /// <summary>
        /// Best line header.
        /// </summary>
        public TrackBestLineHeader BestLineHeader { get; internal set; }

        /// <summary>
        /// List of best line segments.
        /// </summary>
        public IList<TrackBestLineSegment> BestLineSegments { get; internal set; }

        /// <summary>
        /// Computer car setup.
        /// </summary>
        public Setup ComputerCarSetup { get; set; }

        /// <summary>
        /// List of pit lane sections.
        /// </summary>
        public IList<TrackSection> PitLaneSections { get; internal set; }

        //public TrackRawData RawData { get; set; }
    }
}
