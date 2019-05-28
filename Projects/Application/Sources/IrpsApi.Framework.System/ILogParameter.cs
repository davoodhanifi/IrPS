namespace IrpsApi.Framework.System
{
    public interface ILogParameter
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
