namespace IrpsApi.Framework
{
    public interface IPagingOption
    {
        int Skip
        {
            get;
        }

        int Count
        {
            get;
        }
    }
}
