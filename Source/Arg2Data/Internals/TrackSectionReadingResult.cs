using System.Collections.Generic;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal class TrackSectionReadingResult
    {
        public TrackSectionReadingResult(int position, List<TrackSection> trackSections)
        {
            Position = position;
            TrackSections = trackSections;
        }

        public int Position { get; private set; }

        public List<TrackSection> TrackSections { get; }
    }
}
