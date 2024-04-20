using System.IO;
using System.Linq;
using Arg2Data.Internals;
using FluentAssertions;
using Xunit;

namespace Arg2Data.Tests.Internals;

public class ObjectShapesReaderFacts
{
    [Fact]
    public void Montreal_ObjectShapes()
    {
        var trackData = TrackFactsHelper.GetTrackMontreal();
        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        objects.Count.Should().Be(31);
        objects.First().AllData.Length.Should().Be(596);
        objects.Last().AllData.Length.Should().Be(308);
    }

    [Fact]
    public void Monaco_ObjectShapes()
    {
        var trackData = TrackFactsHelper.GetTrackMonaco();
        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        objects.Count.Should().Be(59);
        objects.First().AllData.Length.Should().Be(1796);
        objects.Last().AllData.Length.Should().Be(740);
    }

    [Fact]
    public void Monaco_DataAndHeader_AreSetCorrectly()
    {
        var trackData = TrackFactsHelper.GetTrackMonaco();
        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        objects.First().HeaderIndex.Should().Be(0);
        objects.First().DataIndex.Should().Be(37);
        objects.Last().HeaderIndex.Should().Be(58);
        objects.Last().DataIndex.Should().Be(58);
    }

    [Fact]
    public void Monaco_FirstShape_Has_28_ScaleValues()
    {
        var trackData = TrackFactsHelper.GetTrackMonaco();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.First();
        obj.ScaleValues.Count.Should().Be(28);
        obj.ScaleValues[0].Should().Be(2860);
        obj.ScaleValues[27].Should().Be(2199);
    }

    [Fact]
    public void Monaco_LastShape_Has_14_Points()
    {
        var trackData = TrackFactsHelper.GetTrackMonaco();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        reader.BaseStream.Position = 4110;
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.Last();
        obj.Points.Count.Should().Be(14);

        obj.Points[0].X.Should().Be(5120);
        obj.Points[0].Y.Should().Be(10752);
        obj.Points[0].Z.Should().Be(65024);

        obj.Points[13].X.Should().Be(-3584);
        obj.Points[13].Y.Should().Be(10752);
        obj.Points[13].Z.Should().Be(4608);
    }

    [Fact]
    public void Silverstone_FirstShape_Has_Expected_Offsets()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.First();
        obj.Offset1.Should().Be(15638);
        obj.Offset2.Should().Be(15642);
        obj.Offset3.Should().Be(15646);
        obj.Offset4.Should().Be(15682);
        obj.Offset5.Should().Be(15714);
        obj.Offset6.Should().Be(15674);
        obj.Offset7.Should().Be(15734);
    }

    [Fact]
    public void Silverstone_FirstShape_Has_1_ScaleValue()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.First();
        obj.ScaleValues.Count.Should().Be(1);
        obj.ScaleValues[0].Should().Be(1536);
    }

    [Fact]
    public void Silverstone_SecondShape_Has_3_ScaleValues()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects[1];
        obj.ScaleValues.Count.Should().Be(3);
        obj.ScaleValues[0].Should().Be(960);
        obj.ScaleValues[1].Should().Be(12032);
        obj.ScaleValues[2].Should().Be(10496);
    }

    [Fact]
    public void Silverstone_FirstObject_Has_4_RawPoints()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        reader.BaseStream.Position = 4110;
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.First();
        obj.RawPoints.Count.Should().Be(4);

        // for X
        // [0] 132 -> Scale 0 negative -> -1536
        // [1]  4 -> Scale 0 positive -> 1536
        // [2] 32768 -> use XY from p[0]
        // [3] 32769 -> use XY from p[1]

        obj.RawPoints[0].XCoord.Should().Be(132);  // F1GP=34
        obj.RawPoints[0].ReferencePointValue.Should().Be(132);  // F1GP = 34
        obj.RawPoints[0].ReferencePointFlag.Should().Be(0);
        obj.RawPoints[0].YCoord.Should().Be(0);
        obj.RawPoints[0].ZCoord.Should().Be(0);
        obj.RawPoints[0].Unknown.Should().Be(0);
        obj.RawPoints[1].XCoord.Should().Be(4);  // F1GP = 2
        obj.RawPoints[3].XCoord.Should().Be(-32767);  // or 32769 if ushort?
    }

    [Fact]
    public void Silverstone_FirstObject_Has_4_Points()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        reader.BaseStream.Position = 4110;
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.First();
        obj.Points.Count.Should().Be(4);

        obj.Points[0].X.Should().Be(-1536);
        obj.Points[0].Y.Should().Be(0);
        obj.Points[0].Z.Should().Be(0);

        obj.Points[1].X.Should().Be(1536);
        obj.Points[1].Y.Should().Be(0);
        obj.Points[1].Z.Should().Be(0);

        obj.Points[2].X.Should().Be(-1536);
        obj.Points[2].Y.Should().Be(0);
        obj.Points[2].Z.Should().Be(768);

        obj.Points[3].X.Should().Be(1536);
        obj.Points[3].Y.Should().Be(0);
        obj.Points[3].Z.Should().Be(768);
    }

    [Fact]
    public void Silverstone_SecondObject_Has_12_Points()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        reader.BaseStream.Position = 4110;
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects[1];
        obj.Points.Count.Should().Be(12);

        obj.Points[0].X.Should().Be(-960);
        obj.Points[0].Y.Should().Be(12032);
        obj.Points[0].Z.Should().Be(160);

        obj.Points[1].X.Should().Be(-960);
        obj.Points[1].Y.Should().Be(-10496);
        obj.Points[1].Z.Should().Be(160);

        obj.Points[2].X.Should().Be(960);
        obj.Points[2].Y.Should().Be(12032);
        obj.Points[2].Z.Should().Be(1014);

        obj.Points[11].X.Should().Be(960);
        obj.Points[11].Y.Should().Be(-10496);
        obj.Points[11].Z.Should().Be(160);
    }

    [Fact]
    public void Silverstone_FirstShape_Has_5_Vectors()
    {
        var trackData = TrackFactsHelper.GetTrackSilverstone();

        using var reader = new BinaryReader(MemoryStreamProvider.Open(trackData.Path));
        var objects = ObjectShapesReader.Read(reader, trackData.KnownOffsets.ObjectData);

        var obj = objects.First();
        obj.Vectors.Count.Should().Be(5);
    }
}
