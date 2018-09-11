using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class SortOption : ISortOption
    {
        public string Key
        {
            get;
            private set;
        }

        public SortOrder Order
        {
            get;
            private set;
        }

        public SortOption(string key, SortOrder order)
        {
            Order = order;
            Key = key;
        }
    }
}
