using System.Collections.Generic;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal class TrackBestLineReadResult
    {
        public TrackBestLineReadResult(int positionAfterReading, TrackBestLineHeader header, List<TrackBestLineSegment> bestLineSegments)
        {
            PositionAfterReading = positionAfterReading;
            Header = header;
            BestLineSegments = bestLineSegments;
        }

        public int PositionAfterReading { get; private set; }

        public TrackBestLineHeader Header { get; set; }

        public List<TrackBestLineSegment> BestLineSegments { get; set; }
    }
}
