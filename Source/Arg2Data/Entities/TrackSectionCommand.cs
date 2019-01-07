namespace Arg2Data.Entities
{
    /// <summary>
    /// Track command that adds features to the track section it belongs to.
    /// </summary>
    public class TrackSectionCommand
    {
        internal TrackSectionCommand(byte command, short[] arguments)
        {
            Command = command;
            Arguments = arguments;
        }

        /// <summary>
        /// Gets the command type.
        /// </summary>
        public byte Command { get; }

        /// <summary>
        /// Gets the list of arguments.
        /// </summary>
        public short[] Arguments { get; }
    }
}
