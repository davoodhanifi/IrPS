using System;
using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class RecordInfo : IRecordInfo
    {
        public long Version
        {
            get;
            set;
        }

        public RecordState State
        {
            get;
            set;
        }

        public DateTime? InsertDateTime
        {
            get;
            set;
        }

        public DateTime? UpdateDateTime
        {
            get;
            set;
        }

        public DateTime? DeleteDateTime
        {
            get;
            set;
        }
    }
}
