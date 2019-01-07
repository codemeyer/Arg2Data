namespace Arg2Data.Entities
{
    /// <summary>
    /// Single car setup.
    /// </summary>
    public class Setup
    {
        /// <summary>
        /// Gets the front wing setting. Allowed values between 0 and 64.
        /// </summary>
        public byte FrontWing { get; internal set; } = 40;

        /// <summary>
        /// Gets the rear wing setting. Allowed values between 0 and 64.
        /// </summary>
        public byte RearWing { get; internal set; } = 40;

        /// <summary>
        /// Gets the ratio of the first gear. Must be 16 or greater, and less than GearRatio2.
        /// </summary>
        public byte GearRatio1 { get; internal set; } = 24;

        /// <summary>
        /// Gets the ratio of the second gear. Must be greater than GearRatio1 and less than GearRatio3.
        /// </summary>
        public byte GearRatio2 { get; internal set; } = 32;

        /// <summary>
        /// Gets the ratio of the third gear. Must be greater than GearRatio2 and less than GearRatio4.
        /// </summary>
        public byte GearRatio3 { get; internal set; } = 39;

        /// <summary>
        /// Gets the ratio of the fourth gear. Must be greater than GearRatio3 and less than GearRatio5.
        /// </summary>
        public byte GearRatio4 { get; internal set; } = 46;

        /// <summary>
        /// Gets the ratio of the fifth gear. Must be greater than GearRatio4 and less than GearRatio6.
        /// </summary>
        public byte GearRatio5 { get; internal set; } = 53;

        /// <summary>
        /// Gets the ratio of the sixth gear. Must be greater than GearRatio5 and less than or equal to 80.
        /// </summary>
        public byte GearRatio6 { get; internal set; } = 61;

        /// <summary>
        /// Gets or sets the tyre compound.
        /// </summary>
        public SetupTyreCompound TyreCompound { get; set; } = SetupTyreCompound.C;

        /// <summary>
        /// Gets or sets the brake balance value. Allowed values between -32 (Rear) and 32 (Front).
        /// </summary>
        public sbyte BrakeBalance { get; set; }
    }

    /// <summary>
    /// Tyre compound in setup file. Can be A, B, C or D.
    /// </summary>
    public enum SetupTyreCompound
    {
        /// <summary>
        /// Tyre compound A, the hardest tyre.
        /// </summary>
        A = 0,

        /// <summary>
        /// Tyre compound B.
        /// </summary>
        B = 1,

        /// <summary>
        /// Tyre compound C.
        /// </summary>
        C = 2,

        /// <summary>
        /// Tyre compound D, the softest tyre (except qualifying tyres).
        /// </summary>
        D = 3
    }
}
