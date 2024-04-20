using System;
using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Tests;

public static class TrackFactsHelper
{
    public static TestTrackKnownData GetTrackPhoenix8990()
    {
        return new TestTrackKnownData
        {
            Path = $"{BasePath}Phoenix_USA_1989-1990.dat"
        };
    }

    public static TestTrackKnownData GetTrackAida()
    {
        return new TestTrackKnownData
        {
            Path = $"{BasePath}GP2-F1CT02.DAT",
            //KnownHeaderLength = 28,
            //KnownOffsets = new TrackOffsets
            //{
            //    UnknownLong1 = 368381968,
            //    UnknownLong2 = 368384044,
            //    ChecksumPosition = 16920,
            //    ObjectData = 12362,
            //    TrackData = 14410
            //}
        };
    }

    public static TestTrackKnownData GetTrackMonaco()
    {
        return new TestTrackKnownData
        {
            Path = $"{BasePath}GP2-F1CT04.DAT",
            //KnownHeaderLength = 28,
            KnownOffsets = new TrackOffsets
            {
                UnknownLong1 = 4837616, // 0x49d0f0,
                UnknownLong2 = 4851286, // 0x4a0656,
                                        //ChecksumPosition = 0xe3ae,
                ObjectData = 43216, //0xa8d0,
                TrackData = 46256 //0xb4b0
            }
        };
    }

    public static TestTrackKnownData GetTrackMontreal()
    {
        return new TestTrackKnownData
        {
            Path = $"{BasePath}GP2-F1CT06.DAT",
            KnownHeaderLength = 28,
            KnownOffsets = new TrackOffsets
            {
                UnknownLong1 = 0x4b39c0,
                UnknownLong2 = 0x4b568e,
                ChecksumPosition = 0x8654,
                ObjectData = 0x600c,
                TrackData = 0x699c,
                ComputerCarSetup = 0x7e8e,
                PitLaneData = 0x7eb4
            },
            KnownBestLineSectionDataStart = 30982,
            KnownComputerCarBehaviorStart = 33596
        };
    }

    public static TestTrackKnownData GetTrackSilverstone()
    {
        return new TestTrackKnownData
        {
            Path = $"{BasePath}GP2-F1CT08.DAT",
            KnownHeaderLength = 28,
            KnownOffsets = new TrackOffsets
            {
                UnknownLong1 = 0x4c3c40,
                UnknownLong2 = 0x4c4b32,
                ObjectData = 0x69ee,
                TrackData = 28526      // 0x6f63
            }
        };
    }

    private static string BasePath
    {
        get
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(directory, @"..\..\..\..\..\TestData\tracks\");
        }
    }
}

public class TestTrackKnownData
{
    public string Path { get; set; }

    public TrackOffsets KnownOffsets { get; set; }

    public int KnownHeaderLength { get; set; }

    public int KnownTrackSectionDataStart => KnownOffsets.TrackData + KnownHeaderLength;
    public int KnownBestLineSectionDataStart { get; set; }

    public int KnownComputerCarBehaviorStart { get; set; }
}
