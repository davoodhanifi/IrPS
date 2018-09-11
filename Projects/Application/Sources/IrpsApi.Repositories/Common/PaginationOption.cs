using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public class PaginationOption : IPagingOption
    {
        public int Skip
        {
            get;
        }

        public int Count
        {
            get;
        }

        public PaginationOption(int count)
        {
            Skip = 0;
            Count = count;
        }

        public PaginationOption(int skip, int count)
        {
            Skip = skip;
            Count = count;
        }
    }
}