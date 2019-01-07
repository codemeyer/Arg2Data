using System.Linq;
using Arg2Data.Entities;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class TrackSectionReaderFacts
    {
        private readonly TrackSectionReadingResult _montrealTrackSections;
        private readonly TrackSectionReadingResult _silverstoneTrackSections;

        public TrackSectionReaderFacts()
        {
            var trackDataMontreal = TrackFactsHelper.GetTrackMontreal();
            _montrealTrackSections = TrackSectionReader.Read(trackDataMontreal.Path, trackDataMontreal.KnownTrackSectionDataStart,
                new TrackSectionCommandOptions { Command0xC5Length= 7 });

            var trackDataSilverstone = TrackFactsHelper.GetTrackSilverstone();
            _silverstoneTrackSections = TrackSectionReader.Read(trackDataSilverstone.Path, trackDataSilverstone.KnownTrackSectionDataStart,
                new TrackSectionCommandOptions { Command0xC5Length= 8 });
        }

        [Fact]
        public void Montreal_SectionCount_78()
        {
            _montrealTrackSections.TrackSections.Count.Should().Be(78);
        }

        [Fact]
        public void Montreal_Section0_HasCorrectProperties()
        {
            var section = _montrealTrackSections.TrackSections.First();

            section.Length.Should().Be(9);
            section.Height.Should().Be(0);
            section.Curvature.Should().Be(0);
            section.LeftVergeWidth.Should().Be(8);
            section.RightVergeWidth.Should().Be(16);
            section.HasLeftKerb.Should().BeFalse();
            section.HasRightKerb.Should().BeFalse();
            section.BridgedLeftFence.Should().BeFalse();
            section.BridgedRightFence.Should().BeFalse();
            section.RemoveLeftWall.Should().BeFalse();
            section.RemoveRightWall.Should().BeFalse();
            section.PitLaneEntrance.Should().BeFalse();
            section.PitLaneExit.Should().BeFalse();
        }

        [Fact]
        public void Montreal_Section4_HasCurvature()
        {
            var section = _montrealTrackSections.TrackSections[4];

            section.Curvature.Should().Be(339);
        }

        [Fact]
        public void Montreal_Section11_HasBridgedWallsAndNoWalls()
        {
            var section = _montrealTrackSections.TrackSections[11];

            section.BridgedLeftFence.Should().BeTrue();
            section.BridgedRightFence.Should().BeTrue();
            section.RemoveLeftWall.Should().BeTrue();
            section.RemoveRightWall.Should().BeTrue();
        }

        [Fact]
        public void Montreal_Section11_HasRoadSignArrow()
        {
            var section = _montrealTrackSections.TrackSections[11];

            section.RoadSignArrow.Should().BeTrue();
        }

        [Fact]
        public void Montreal_Section14_HasHighRightKerb()
        {
            var section = _montrealTrackSections.TrackSections[14];

            section.HasRightKerb.Should().BeTrue();
            section.KerbHeight.Should().Be(KerbHeight.High);
        }

        [Fact]
        public void Montreal_Section17_HasHeight()
        {
            var section = _montrealTrackSections.TrackSections[17];

            section.Height.Should().Be(37);
        }

        [Fact]
        public void Montreal_Section22_HasHighLeftKerb()
        {
            var section = _montrealTrackSections.TrackSections[22];

            section.HasLeftKerb.Should().BeTrue();
            section.KerbHeight.Should().Be(KerbHeight.High);
        }

        [Fact]
        public void Montreal_Section23_HasLowRightKerb()
        {
            var section = _montrealTrackSections.TrackSections[23];

            section.HasRightKerb.Should().BeTrue();
            section.KerbHeight.Should().Be(KerbHeight.Low);
        }

        [Fact]
        public void Montreal_Section40_HasRoadSignsCombo()
        {
            var section = _montrealTrackSections.TrackSections[40];

            section.RoadSignArrow.Should().BeTrue();
            section.RoadSigns.Should().BeTrue();
            section.RoadSignArrow100.Should().BeFalse();
        }

        [Fact]
        public void Montreal_Section44_HasUnknown3Set()
        {
            var section = _montrealTrackSections.TrackSections[44];

            section.Unknown3.Should().BeTrue();
            section.Unknown1.Should().BeFalse();
            section.Unknown2.Should().BeFalse();
            section.Unknown4.Should().BeFalse();
        }

        [Fact]
        public void Montreal_Section49_HasRoadSigns300200100()
        {
            var section = _montrealTrackSections.TrackSections[49];

            section.RoadSigns.Should().BeTrue();
            section.RoadSignArrow.Should().BeFalse();
            section.RoadSignArrow100.Should().BeFalse();
        }


        [Fact]
        public void Montreal_Section0_HasCommands()
        {
            var section = _montrealTrackSections.TrackSections[0];

            section.Commands.Count.Should().Be(26);
        }

        [Fact]
        public void Montreal_Section5_Command1()
        {
            var command = _montrealTrackSections.TrackSections[5].Commands[1];

            command.Command.Should().Be(0x80);
            command.Arguments[0].Should().Be(0);
            command.Arguments[1].Should().Be(800);
        }

        [Fact]
        public void Montreal_Section77_Command1()
        {
            var command = _montrealTrackSections.TrackSections[77].Commands[1];

            command.Command.Should().Be(0xAF);
            command.Arguments[0].Should().Be(0);
            command.Arguments[1].Should().Be(-16384);
            command.Arguments[2].Should().Be(16384);
        }

        [Fact]
        public void Silverstone_SectionCount_125()
        {
            _silverstoneTrackSections.TrackSections.Count.Should().Be(125);
        }
    }
}
