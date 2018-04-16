using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests
{
    public class TrackReaderFacts
    {
        // these tests are a form of integration test, making sure that all data reading glues together correctly

        [Fact]
        public void AidaDataIntegration()
        {
            var trackData = TrackFactsHelper.GetTrackAida();
            var track = TrackReader.Read(trackData.Path);

            track.TrackSections.Count.Should().Be(70);
            track.PitLaneSections.Count.Should().Be(24);
            track.BestLineSegments.Count.Should().Be(53);
        }

        [Fact]
        public void MonacoDataIntegration()
        {
            var trackData = TrackFactsHelper.GetTrackMonaco();
            var track = TrackReader.Read(trackData.Path);

            track.TrackSections.Count.Should().Be(132);
            track.PitLaneSections.Count.Should().Be(22);
            track.BestLineSegments.Count.Should().Be(68);
        }

        [Fact]
        public void MontrealDataIntegration()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();
            var track = TrackReader.Read(trackData.Path);

            track.TrackSections.Count.Should().Be(78);
            track.PitLaneSections.Count.Should().Be(26);
            track.BestLineSegments.Count.Should().Be(48);
            track.ObjectShapes.Count.Should().Be(31);
            track.ObjectSettings.Count.Should().Be(153);

            track.BestLineHeader.LineStartX.Should().Be(4);
            track.ComputerCarSetup.FrontWing.Should().Be(11);
        }

        [Fact]
        public void SilverstoneDataIntegration()
        {
            var trackData = TrackFactsHelper.GetTrackSilverstone();
            var track = TrackReader.Read(trackData.Path);

            track.TrackSections.Count.Should().Be(125);
        }
    }
}
