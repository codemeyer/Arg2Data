using System.IO;
using System.Linq;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals
{
    public class ComputerCarLineReaderFacts
    {
        [Fact]
        public void MontrealBestLine()
        {
            var trackData = TrackFactsHelper.GetTrackMontreal();
            using (var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path)))
            {
                var bestLines = ComputerCarLineReader.Read(reader, trackData.KnownBestLineSectionDataStart);

                bestLines.Header.LineStartX.Should().Be(4);
                bestLines.Header.LineStartXHigh.Should().Be(208);
                bestLines.Header.LineStartY.Should().Be(0);
                //bestLines.Header.LineStartYHigh.Should().Be(0);
                bestLines.Header.Unknown1.Should().Be(65354);
                bestLines.Header.Unknown2.Should().Be(0);
                bestLines.Header.Unknown3.Should().Be(0);
                bestLines.Header.Unknown4.Should().Be(0);

                var firstSegment = bestLines.Segments[0];
                firstSegment.Length.Should().Be(26);
                firstSegment.Command.Should().Be(0x50);
                firstSegment.SignScale.Should().Be(1);
                firstSegment.Radius.Should().Be(65092);

                var segment2 = bestLines.Segments[1];
                segment2.Length.Should().Be(37);
                segment2.Radius.Should().Be(0);

                var segment3 = bestLines.Segments[2];
                segment3.Correction.Should().Be(185);

                var segment6 = bestLines.Segments[5];
                segment6.Length.Should().Be(17);
                segment6.Command.Should().Be(0x70);
                segment6.Radius.Should().Be(8280);
                segment6.Sign0x70.Should().Be(0);
                segment6.Radius0x70.Should().Be(9650);

                var lastSegment = bestLines.Segments.Last();
                lastSegment.Length.Should().Be(48);
                lastSegment.Command.Should().Be(0x50);
                lastSegment.Radius.Should().Be(48032);
            }
        }
    }
}
