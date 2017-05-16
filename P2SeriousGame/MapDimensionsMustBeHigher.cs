using System;
using System.Runtime.Serialization;

namespace P2SeriousGame
{
    [Serializable]
    public class MapDimensionsMustBeHigher : Exception
    {
        private string v;
        private int value;

        public MapDimensionsMustBeHigher()
        {
        }

        public MapDimensionsMustBeHigher(string message) : base(message)
        {
        }

        public MapDimensionsMustBeHigher(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MapDimensionsMustBeHigher(int value, string v)
        {
            this.value = value;
            this.v = v;
        }

        protected MapDimensionsMustBeHigher(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}