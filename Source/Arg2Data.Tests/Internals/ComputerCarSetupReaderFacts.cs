using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class ComputerCarSetupReaderFacts
    {
        [Fact]
        public void Montreal_Setup()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();
            var setup = ComputerCarSetupReader.Read(trackData.Path, trackData.KnownOffsets.ComputerCarSetup);

            setup.Setup.FrontWing.Should().Be(11);
            setup.Setup.RearWing.Should().Be(10);
            setup.Setup.GearRatio1.Should().Be(28);
            setup.Setup.GearRatio2.Should().Be(35);
            setup.Setup.GearRatio3.Should().Be(42);
            setup.Setup.GearRatio4.Should().Be(49);
            setup.Setup.GearRatio5.Should().Be(55);
            setup.Setup.GearRatio6.Should().Be(61);
        }
    }
}
