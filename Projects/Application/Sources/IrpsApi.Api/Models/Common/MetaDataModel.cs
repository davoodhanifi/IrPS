using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Common
{
    [DataContract(Name = "meta")]
    internal class MetaDataModel
    {
        [DataMember(Name = "total_items_count")]
        public int TotalItemsCount
        {
            get;
            set;
        }
    }
}
