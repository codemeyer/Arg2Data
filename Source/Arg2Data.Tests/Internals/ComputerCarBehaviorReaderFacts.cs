using System.IO;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class ComputerCarBehaviorReaderFacts
    {
        [Fact]
        public void Read_MontrealTrack_HasExpectedBehaviorData()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();

            using (var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path)))
            {
                var behavior = ComputerCarBehaviorReader.Read(reader, trackData.KnownComputerCarBehaviorStart);

                behavior.LapCount.Should().Be(69);
            }
        }
    }
}
