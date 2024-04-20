using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests;

public class TrackReaderFacts
{
    // these tests are a form of integration test, making sure that all data reading glues together correctly

    [Fact]
    public void AidaDataIntegration()
    {
        var trackData = TrackFactsHelper.GetTrackAida();
        var trackReader = new TrackReader();
        var track = trackReader.Read(trackData.Path);

        track.TrackSections.Count.Should().Be(70);
        track.PitLaneSections.Count.Should().Be(24);
        track.ComputerCarLineSegments.Count.Should().Be(53);
    }

    [Fact]
    public void MonacoDataIntegration()
    {
        var trackData = TrackFactsHelper.GetTrackMonaco();
        var trackReader = new TrackReader();
        var track = trackReader.Read(trackData.Path);

        track.TrackSections.Count.Should().Be(132);
        track.PitLaneSections.Count.Should().Be(22);
        track.ComputerCarLineSegments.Count.Should().Be(68);

        track.TrackSettings.LapCount.Should().Be(78);
    }

    [Fact]
    public void MontrealDataIntegration()
    {
        var trackData = TrackFactsHelper.GetTrackMontreal();
        var trackReader = new TrackReader();
        var track = trackReader.Read(trackData.Path);

        track.TrackSections.Count.Should().Be(79, "contains 78 normal sections and 1 with only command data");
        track.PitLaneSections.Count.Should().Be(26);
        track.ComputerCarLineSegments.Count.Should().Be(48);
        track.ObjectShapes.Count.Should().Be(31);
        track.ObjectSettings.Count.Should().Be(153);

        track.ComputerCarLineHeader.LineStartX.Should().Be(4);
        track.ComputerCarSetup.FrontWing.Should().Be(11);

        track.TrackSettings.LapCount.Should().Be(69);
    }

    [Fact]
    public void SilverstoneDataIntegration()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();
        var trackReader = new TrackReader();
        var track = trackReader.Read(trackData.Path);

        track.TrackSections.Count.Should().Be(126, "contains 125 sections and 1 with only command data");
    }

    [Fact]
    public void Phoenix8990_Contains_22_PitLane_Sections()
    {
        var trackData = TrackFactsHelper.GetTrackPhoenix8990();
        var trackReader = new TrackReader();
        var track = trackReader.Read(trackData.Path);

        track.PitLaneSections.Count.Should().Be(22);
        track.PitLaneSections[20].Length.Should().Be(0);
    }
}
