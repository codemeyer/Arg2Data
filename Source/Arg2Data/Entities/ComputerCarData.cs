namespace Arg2Data.Entities
{
    /// <summary>
    /// Contains various properties that affect the computer car (and player car) in various ways.
    /// </summary>
    public class ComputerCarData
    {
        /// <summary>
        /// Gets or sets the grip factor.
        /// </summary>
        public short GripFactor { get; set; }

        /// <summary>
        /// Gets or sets the acceleration factor.
        /// </summary>
        public short Acceleration { get; set; }

        /// <summary>
        /// Gets or sets the air resistance.
        /// </summary>
        public short AirResistance { get; set; }

        /// <summary>
        /// Gets or sets the fuel load?
        /// </summary>
        public short FuelLoad { get; set; }
    }
}
