namespace IrpsApi.Framework.Operation
{
    public interface IRequestParameter
    {
        string Key
        {
            get;
            set;
        }

        object Value
        {
            get;
            set;
        }
    }
}
