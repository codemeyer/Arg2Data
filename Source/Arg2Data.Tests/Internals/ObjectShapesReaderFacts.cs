using System.Linq;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class ObjectShapesReaderFacts
    {
        [Fact]
        public void Montreal_ObjectShapes()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();
            var objects = ObjectShapesReader.Read(trackData.Path, trackData.KnownOffsets.ObjectData);

            objects.Count.Should().Be(31);
            objects.First().AllData.Length.Should().Be(596);
            objects.Last().AllData.Length.Should().Be(308);
        }

        [Fact]
        public void Monaco_ObjectShapes()
        {
            var trackData = TrackFactsHelper.GetTrackMonaco();
            var objects = ObjectShapesReader.Read(trackData.Path, trackData.KnownOffsets.ObjectData);

            objects.Count.Should().Be(59);
            objects.First().AllData.Length.Should().Be(1796);
            objects.Last().AllData.Length.Should().Be(740);
        }

        [Fact]
        public void Monaco_DataAndHeader_AreSetCorrectly()
        {
            var trackData = TrackFactsHelper.GetTrackMonaco();
            var objects = ObjectShapesReader.Read(trackData.Path, trackData.KnownOffsets.ObjectData);

            objects.First().HeaderIndex.Should().Be(0);
            objects.First().DataIndex.Should().Be(37);
            objects.Last().HeaderIndex.Should().Be(58);
            objects.Last().DataIndex.Should().Be(58);
        }
    }
}
