using System.IO;
using Arg2Data.Entities;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals;

public class TrackSectionHeaderFacts
{
    [Fact]
    public void MontrealHeader()
    {
        var trackData = TrackFactsHelper.GetTrackMontreal();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var header = TrackSectionHeaderReader.Read(reader, trackData.KnownOffsets.TrackData);

        header.FirstSectionAngle.Should().Be(65354);
        header.FirstSectionHeight.Should().Be(0);
        header.TrackCenterX.Should().Be(13000);
        header.TrackCenterZ.Should().Be(1328);
        header.TrackCenterY.Should().Be(-4299);
        header.StartWidth.Should().Be(1203);

        header.PoleSide.Should().Be(TrackSide.Left);
        header.PitsSide.Should().Be(TrackSide.Left);

        header.CommandLength0xC5.Should().Be(7);

        header.LeftVergeStartWidth.Should().Be(8);
        header.RightVergeStartWidth.Should().Be(20);
    }

    [Fact]
    public void SilverstoneHeader()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();
        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var header = TrackSectionHeaderReader.Read(reader, trackData.KnownOffsets.TrackData);

        header.PitsSide.Should().Be(TrackSide.Right);

        header.CommandLength0xC5.Should().Be(8);
    }
}
