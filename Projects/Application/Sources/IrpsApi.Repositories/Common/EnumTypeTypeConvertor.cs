using System;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class EnumTypeTypeConvertor<TEnum> : IFilterDataConvertor where TEnum : struct, IConvertible
    {
        public object ConvertToDatabaseValue(object data)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            return ((IConvertible)data).ToInt32(null);
        }
    }
}
