using System;

namespace IrpsApi.Framework
{
    public interface IRecord : IEntity
    {
        long RecordVersion
        {
            get;
            set;
        }

        RecordState RecordState
        {
            get;
            set;
        }

        DateTime? RecordInsertDateTime
        {
            get;
            set;
        }

        DateTime? RecordUpdateDateTime
        {
            get;
            set;
        }

        DateTime? RecordDeleteDateTime
        {
            get;
            set;
        }
    }
}
