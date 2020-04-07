using System;
using System.IO;
using Arg2Data.Entities;
using Arg2Data.Internals;
using Arg2Data.IO;

namespace Arg2Data
{
    /// <summary>
    /// Track reader.
    /// </summary>
    public class TrackReader
    {
        /// <summary>
        /// Reads the track file at the specified path and returns a Track object.
        /// </summary>
        /// <param name="path">Path to GP2 track file.</param>
        /// <returns>Track object.</returns>
        public Track Read(string path)
        {
            var track = new Track();

            using (var reader = new BinaryReader(StreamProvider(path)))
            {
                track.Offsets = OffsetReader.Read(path);

                track.ObjectShapes = ObjectShapesReader.Read(path, track.Offsets.ObjectData);
                track.ObjectSettings = TrackObjectSettingsReader.Read(path, track.Offsets.ObjectData, track.Offsets.TrackData);
                track.TrackDataHeader = TrackSectionHeaderReader.Read(path, track.Offsets.TrackData);

                var options = new TrackSectionCommandOptions();
                options.Command0xC5Length = track.TrackDataHeader.CommandLength0xC5;

                var sectionReading = TrackSectionReader.Read(path, track.Offsets.TrackData + track.TrackDataHeader.GetHeaderLength(), options);
                track.TrackSections = sectionReading.TrackSections;

                var bestLines = BestLineReader.Read(path, sectionReading.Position);
                track.ComputerCarLineHeader = bestLines.Header;
                track.ComputerCarLineSegments = bestLines.Segments;

                var setup = ComputerCarDataReader.Read(reader, track.Offsets.ComputerCarSetup);
                track.ComputerCarSetup = setup.Setup;

                var pitlaneResult = TrackSectionReader.Read(path, track.Offsets.PitLaneData, options);
                track.PitLaneSections = pitlaneResult.TrackSections;

                reader.BaseStream.Position = pitlaneResult.Position;

                // read until 0xFF 0xFF, i.e. skip camera definitions
                bool previousWas255 = false;

                while (true)
                {
                    byte byte1 = reader.ReadByte();

                    if (byte1 == 0xFF)
                    {
                        if (previousWas255)
                        {
                            break;
                        }

                        previousWas255 = true;
                    }
                    else
                    {
                        previousWas255 = false;
                    }
                }

                var posBefore = reader.BaseStream.Position;

                var behavior = ComputerCarBehaviorReader.Read(reader, (int)reader.BaseStream.Position);
                track.ComputerCarBehavior = behavior;

                reader.BaseStream.Position = posBefore;

                var settings = TrackSettingsReader.Read(reader, (int)reader.BaseStream.Position);
                track.TrackSettings = settings;

                return track;
            }
        }

        /// <summary>
        /// Default FileStream provider. Can be overridden in tests.
        /// </summary>
        internal Func<string, Stream> StreamProvider = FileStreamProvider.Open;
    }
}
