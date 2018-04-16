namespace Arg2Data.Entities
{
    /// <summary>
    /// Object 3D shape.
    /// </summary>
    public class TrackObjectShape
    {
        /// <summary>
        /// Initializes a new instance of a TrackObjectShape.
        /// </summary>
        /// <param name="headerIndex"></param>
        /// <param name="dataIndex"></param>
        internal TrackObjectShape(int headerIndex, int dataIndex)
        {
            HeaderIndex = headerIndex;
            DataIndex = dataIndex;
        }

        /// <summary>
        /// Gets the header index of the object shape.
        /// </summary>
        public int HeaderIndex { get; }

        /// <summary>
        /// Gets the data index of the object shape.
        /// </summary>
        public int DataIndex { get; }

        /// <summary>
        /// Gets all the raw object data.
        /// </summary>
        public byte[] AllData { get; internal set; }
    }
}
