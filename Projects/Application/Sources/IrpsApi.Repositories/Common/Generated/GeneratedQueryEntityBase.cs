using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryEntityBase : EntityBase
    {
        [PrimaryKey]
        [Identity]
        [OutputColumn]
        [SqlColumnType("INT")]
        public override string Id
        {
            get;
            set;
        }
    }
}