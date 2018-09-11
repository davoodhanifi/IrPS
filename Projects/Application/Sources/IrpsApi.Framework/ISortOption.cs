namespace IrpsApi.Framework
{
    public interface ISortOption
    {
        string Key
        {
            get;
        }

        SortOrder Order
        {
            get;
        }
    }
}
