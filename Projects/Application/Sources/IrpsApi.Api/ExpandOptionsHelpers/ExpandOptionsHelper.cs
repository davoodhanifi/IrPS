using Mabna.WebApi.Common;

namespace IrpsApi.Api.ExpandOptionsHelpers
{
    internal static class ExpandOptionsHelper
    {
        public static bool TryGetExpandOption<T>(this IExpandOptionCollection collection, string key, out IExpandOption<T> expandOption)
        {
            expandOption = collection?.GetOption<T>(key);
            return expandOption != null;
        }
    }
}