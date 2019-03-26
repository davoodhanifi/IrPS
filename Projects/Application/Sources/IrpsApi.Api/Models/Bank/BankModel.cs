using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Bank
{
    [DataContract(Name = "bank")]
    public class BankModel : RecordModel
    {
        [DataMember(Name = "key")]
        public string Key
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "english_name")]
        public string NameEn
        {
            get;
            set;
        }
    }
}
