using Arg2Data.Entities;

namespace Arg2Data.Internals;

internal static class ComputerCarBehaviorReader
{
    internal static ComputerCarBehavior Read(BinaryReader reader, int position)
    {
        return new ComputerCarBehavior();
    }
}
