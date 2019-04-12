using System;
using System.Collections.Generic;

namespace IrpsApi.Framework.Operation
{
    public interface IRequest : IRecord
    {
        string AccountId
        {
            get;
            set;
        }

        string TypeId
        {
            get;
            set;
        }

        DateTime DateTime
        {
            get;
            set;
        }

        string StatusId
        {
            get;
            set;
        }

        string Comments
        {
            get;
            set;
        }

        IEnumerable<IRequestParameter> Parameters
        {
            get;
            set;
        }
    }
}
