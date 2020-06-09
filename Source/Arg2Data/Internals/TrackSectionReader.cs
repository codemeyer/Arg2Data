using System.Collections.Generic;
using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class TrackSectionReader
    {
        public static TrackSectionReadingResult Read(BinaryReader reader, int startPosition, TrackSectionCommandOptions options)
        {
            var sections = new List<TrackSection>();

            reader.BaseStream.Position = startPosition;

            var currentSection = new TrackSection();

            while (true)
            {
                byte byte1 = reader.ReadByte();
                byte byte2 = reader.ReadByte();

                if (byte1 == 255 && byte2 == 255)
                {
                    if (currentSection.Commands.Count > 0)
                    {
                        // section with length 0, but has commands
                        sections.Add(currentSection);
                    }

                    break;
                }

                if (byte2 > 0)
                {
                    // is command
                    var argCount = TrackSectionCommandFactory.GetArgumentCountForCommand(byte2, options);

                    short[] arguments = new short[argCount];

                    arguments[0] = byte1;

                    for (int i = 1; i < argCount; i++)
                    {
                        arguments[i] = reader.ReadInt16();
                    }

                    currentSection.Commands.Add(TrackSectionCommandFactory.Get(byte2, arguments));

                    continue;
                }

                // section
                currentSection.Length = byte1;
                currentSection.Curvature = reader.ReadInt16();
                currentSection.Height = reader.ReadInt16();
                currentSection.Flags = reader.ReadInt16();
                currentSection.RightVergeWidth = reader.ReadByte();
                currentSection.LeftVergeWidth = reader.ReadByte();
                sections.Add(currentSection);

                currentSection = new TrackSection();
            }

            int position = (int)reader.BaseStream.Position;
            return new TrackSectionReadingResult(position, sections);
        }
    }

    internal class TrackSectionCommandOptions
    {
        internal int Command0xC5Length { get; set; }
    }
}
