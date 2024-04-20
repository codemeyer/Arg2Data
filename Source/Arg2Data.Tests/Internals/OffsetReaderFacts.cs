using System.IO;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals;

public class OffsetReaderFacts
{
    [Fact]
    public void MontrealOffsetsReader()
    {
        var trackData = TrackFactsHelper.GetTrackMontreal();
        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var offsets = OffsetReader.Read(reader);

        offsets.UnknownLong1.Should().Be(trackData.KnownOffsets.UnknownLong1);
        offsets.UnknownLong2.Should().Be(trackData.KnownOffsets.UnknownLong2);
        offsets.ChecksumPosition.Should().Be(trackData.KnownOffsets.ChecksumPosition);
        offsets.ObjectData.Should().Be(trackData.KnownOffsets.ObjectData);
        offsets.TrackData.Should().Be(trackData.KnownOffsets.TrackData);
        offsets.PitLaneData.Should().Be(trackData.KnownOffsets.PitLaneData);
    }
}
