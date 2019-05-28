using System;
using System.Collections.Generic;

namespace IrpsApi.Framework.System
{
    public interface ILog : IRecord
    {
        string Source
        {
            get;
            set;
        }

        string LevelId
        {
            get;
            set;
        }

        string AccountId
        {
            get;
            set;
        }

        DateTime DateTime
        {
            get;
            set;
        }

        string Action
        {
            get;
            set;
        }

        IEnumerable<ILogParameter> Parameters
        {
            get;
            set;
        }
    }
}
