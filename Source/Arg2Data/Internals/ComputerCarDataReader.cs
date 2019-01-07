using System;
using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class ComputerCarDataReader
    {
        internal static ComputerCarDataReadingResult Read(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position;

            var setup = new Setup
            {
                FrontWing = Convert.ToByte(reader.ReadByte() - 151),
                RearWing = Convert.ToByte(reader.ReadByte() - 151),
                GearRatio1 = Convert.ToByte(reader.ReadByte() - 151),
                GearRatio2 = Convert.ToByte(reader.ReadByte() - 151),
                GearRatio3 = Convert.ToByte(reader.ReadByte() - 151),
                GearRatio4 = Convert.ToByte(reader.ReadByte() - 151),
                GearRatio5 = Convert.ToByte(reader.ReadByte() - 151),
                GearRatio6 = Convert.ToByte(reader.ReadByte() - 151),
                TyreCompound = GetTyreCompound(Convert.ToByte(reader.ReadByte() - 52)),
                BrakeBalance = Convert.ToSByte(reader.ReadSByte())
            };

            var data = new ComputerCarData();
            data.GripFactor = reader.ReadInt16();
            reader.BaseStream.Position += 6;
            data.Acceleration = reader.ReadInt16();
            data.AirResistance = reader.ReadInt16();
            reader.BaseStream.Position += 4;
            data.FuelLoad = reader.ReadInt16();

            return new ComputerCarDataReadingResult
            {
                Setup = setup,
                ComputerCarData = data
            };
        }

        private static SetupTyreCompound GetTyreCompound(byte rawValue)
        {
            return (SetupTyreCompound)Enum.Parse(typeof(SetupTyreCompound), rawValue.ToString());
        }
    }
}
