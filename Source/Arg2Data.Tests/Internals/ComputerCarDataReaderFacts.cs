using System.IO;
using Arg2Data.Entities;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class ComputerCarDataReaderFacts
    {
        [Fact]
        public void Read_MontrealTrack_HasExpectedSetup()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();

            using (var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path)))
            {
                var result = ComputerCarDataReader.Read(reader, trackData.KnownOffsets.ComputerCarSetup);
                var setup = result.Setup;

                setup.FrontWing.Should().Be(11);
                setup.RearWing.Should().Be(10);
                setup.GearRatio1.Should().Be(28);
                setup.GearRatio2.Should().Be(35);
                setup.GearRatio3.Should().Be(42);
                setup.GearRatio4.Should().Be(49);
                setup.GearRatio5.Should().Be(55);
                setup.GearRatio6.Should().Be(61);
                setup.TyreCompound.Should().Be(SetupTyreCompound.C);
                setup.BrakeBalance.Should().Be(0);
            }
        }

        [Fact]
        public void Read_MontrealTrack_HasExpectedData()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();

            using (var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path)))
            {
                var result = ComputerCarDataReader.Read(reader, trackData.KnownOffsets.ComputerCarSetup);
                var data = result.ComputerCarData;

                data.GripFactor.Should().Be(16384);
                data.Acceleration.Should().Be(16384);
                data.AirResistance.Should().Be(16384);
                data.FuelLoad.Should().Be(5203);
            }
        }
    }
}
