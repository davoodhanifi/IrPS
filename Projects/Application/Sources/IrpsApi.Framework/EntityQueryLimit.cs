using System;

namespace IrpsApi.Framework
{
    public class EntityQueryLimit
    {
        public int Count
        {
            get;
            set;
        }

        public int Skip
        {
            get;
            set;
        }

        public EntityQueryLimit()
        {
        }

        public EntityQueryLimit(int count)
        {
            Count = count;
        }

        public EntityQueryLimit(int count, int skip)
        {
            Count = count;
            Skip = skip;
        }
    }
}
