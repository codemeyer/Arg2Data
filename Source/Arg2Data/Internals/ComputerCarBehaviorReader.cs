using System.IO;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class ComputerCarBehaviorReader
    {
        internal static ComputerCarBehavior Read(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position + 22;

            return new ComputerCarBehavior
            {
                LapCount = reader.ReadInt16()
            };
        }
    }
}
