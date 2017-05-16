using System;
using System.Runtime.Serialization;

namespace P2SeriousGame
{
    [Serializable]
    internal class MapDimensionsMustBeHigher : Exception
    {
        public MapDimensionsMustBeHigher()
        {
        }

        public MapDimensionsMustBeHigher(int dimension, string message) : base(message)
        {
        }

        public MapDimensionsMustBeHigher(string message) : base(message)
        {
        }

        public MapDimensionsMustBeHigher(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MapDimensionsMustBeHigher(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int Dimension { get; set; }
    }
}