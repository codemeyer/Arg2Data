using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class OffsetReaderFacts
    {
        [Fact]
        public void MontrealOffsets()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();
            var offsets = OffsetReader.Read(trackData.Path);

            offsets.UnknownLong1.Should().Be(trackData.KnownOffsets.UnknownLong1);
            offsets.UnknownLong2.Should().Be(trackData.KnownOffsets.UnknownLong2);
            offsets.ChecksumPosition.Should().Be(trackData.KnownOffsets.ChecksumPosition);
            offsets.ObjectData.Should().Be(trackData.KnownOffsets.ObjectData);
            offsets.TrackData.Should().Be(trackData.KnownOffsets.TrackData);
            offsets.PitLaneData.Should().Be(trackData.KnownOffsets.PitLaneData);
        }
    }
}
