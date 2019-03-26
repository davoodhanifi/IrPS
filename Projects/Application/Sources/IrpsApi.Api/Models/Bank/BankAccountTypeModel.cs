using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Bank
{
    [DataContract(Name = "bank_account_type")]
    public class BankAccountTypeModel : RecordModel
    {
        [DataMember(Name = "title")]
        public string Title
        {
            get;
            set;
        }

        [DataMember(Name = "english_title")]
        public string TitleEn
        {
            get;
            set;
        }
    }
}
