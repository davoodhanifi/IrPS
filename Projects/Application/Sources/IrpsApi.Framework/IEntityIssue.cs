using System;

namespace IrpsApi.Framework
{
    public interface IEntityIssue
    {
        IEntity Entity
        {
            get;
        }

        EntityIssueType Type
        {
            get;
        }

        string Message
        {
            get;
        }

        string FieldName
        {
            get;
        }
    }
}
