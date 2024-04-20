using Arg2Data.Entities;

namespace Arg2Data.Internals;

internal class TrackComputerCarLineReadingResult
{
    public TrackComputerCarLineReadingResult(int positionAfterReading, TrackComputerCarLineHeader header, List<TrackComputerCarLineSegment> segments)
    {
        PositionAfterReading = positionAfterReading;
        Header = header;
        Segments = segments;
    }

    public int PositionAfterReading { get; private set; }

    public TrackComputerCarLineHeader Header { get; set; }

    public List<TrackComputerCarLineSegment> Segments { get; set; }
}
