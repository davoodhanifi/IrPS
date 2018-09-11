using System.Linq;
using Dapper;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Common.DapperConfiguration
{
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
                {
                new CustomPropertyTypeMap(
                   typeof(T),
                   (type, columnName) =>
                       type.GetProperties().FirstOrDefault(prop =>
                           prop.GetCustomAttributes(false)
                               .OfType<ColumnAliasAttribute>()
                               .Any(attr => attr.Name == columnName)
                           )
                   ),
                new DefaultTypeMap(typeof(T))
                })
        {
        }
    }
}