using System.Collections.Generic;
using Arg2Data.Entities;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class ObjectSettingsReaderFacts
    {
        private readonly IList<TrackObjectSettings> _montrealObjectSettings;

        private readonly List<TrackObjectSettings> _silverstoneObjectSettings;

        public ObjectSettingsReaderFacts()
        {
            var trackDataMontreal = TrackFactsHelper.GetTrackMontreal();
            _montrealObjectSettings = TrackObjectSettingsReader.Read(trackDataMontreal.Path,
                trackDataMontreal.KnownOffsets.ObjectData, trackDataMontreal.KnownOffsets.TrackData);

            var trackDataSilverstone = TrackFactsHelper.GetTrackSilverstone();
            _silverstoneObjectSettings = TrackObjectSettingsReader.Read(trackDataSilverstone.Path,
                trackDataSilverstone.KnownOffsets.ObjectData, trackDataSilverstone.KnownOffsets.TrackData);
        }

        [Fact]
        public void Montreal_ObjectSettings()
        {
            _montrealObjectSettings.Count.Should().Be(153);
        }

        [Fact]
        public void Montreal_Object17()
        {
            var setting = _montrealObjectSettings[17];

            setting.Id.Should().Be(40);
            setting.Id2.Should().Be(0);
            setting.Offset.Should().Be(272);
            setting.DetailLevel.Should().Be(0);
            setting.Height.Should().Be(160);
            setting.DistanceFromTrack.Should().Be(-2000);
            setting.AngleY.Should().Be(0);
            setting.AngleX.Should().Be(0);
            setting.Unknown.Should().Be(1312);
            setting.Unknown2.Should().Be(2);
        }

        [Fact]
        public void Silverstone_ObjectSettings()
        {
            _silverstoneObjectSettings.Count.Should().Be(88);
        }
    }
}
