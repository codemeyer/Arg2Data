﻿using System;
using System.Linq;
using Arg2Data.Entities;
using Arg2Data.IO;

namespace Arg2Data.Internals
{
    internal class ComputerCarSetupReaderResult
    {
        public Setup Setup { get; set; }
        public byte[] RawData { get; set; }
    }

    internal static class ComputerCarSetupReader
    {
        internal static ComputerCarSetupReaderResult Read(string path, int position)
        {
            var trackFileReader = new FileReader(path);
            byte[] setupBytes = trackFileReader.ReadBytes(position, 38);

            var setup = new Setup
            {
                FrontWing = Convert.ToByte(setupBytes[0] - 151),
                RearWing = Convert.ToByte(setupBytes[1] - 151),
                GearRatio1 = Convert.ToByte(setupBytes[2] - 151),
                GearRatio2 = Convert.ToByte(setupBytes[3] - 151),
                GearRatio3 = Convert.ToByte(setupBytes[4] - 151),
                GearRatio4 = Convert.ToByte(setupBytes[5] - 151),
                GearRatio5 = Convert.ToByte(setupBytes[6] - 151),
                GearRatio6 = Convert.ToByte(setupBytes[7] - 151),
            };

            return new ComputerCarSetupReaderResult
            {
                Setup = setup,
                RawData = setupBytes.Skip(8).ToArray()
            };
        }
    }
}
