using Arg2Data.Entities;
using Arg2Data.Internals;

namespace Arg2Data
{
    /// <summary>
    /// Track reader.
    /// </summary>
    public static class TrackReader
    {
        /// <summary>
        /// Reads the track file at the specified path and returns a Track object.
        /// </summary>
        /// <param name="path">Path to GP2 track file.</param>
        /// <returns>Track object.</returns>
        public static Track Read(string path)
        {
            var track = new Track();

            track.Offsets = OffsetReader.Read(path);

            track.ObjectShapes = ObjectShapesReader.Read(path, track.Offsets.ObjectData);
            track.ObjectSettings = TrackObjectSettingsReader.Read(path, track.Offsets.ObjectData, track.Offsets.TrackData);
            track.TrackDataHeader = TrackSectionHeaderReader.Read(path, track.Offsets.TrackData);

            var options = new TrackCommandOptions();
            options.Command0xC5Length = track.TrackDataHeader.CommandLength0xC5;

            var sectionReading = TrackSectionReader.Read(path, track.Offsets.TrackData + track.TrackDataHeader.GetHeaderLength(), options);
            track.TrackSections = sectionReading.TrackSections;

            var bestLines = BestLineReader.Read(path, sectionReading.Position);
            track.BestLineHeader = bestLines.Header;
            track.BestLineSegments = bestLines.BestLineSegments;

            var setup = ComputerCarSetupReader.Read(path, track.Offsets.ComputerCarSetup);
            track.ComputerCarSetup = setup.Setup;

            var pitlaneResult = TrackSectionReader.Read(path, track.Offsets.PitLaneData, options);
            track.PitLaneSections = pitlaneResult.TrackSections;

            return track;
        }
    }
}
