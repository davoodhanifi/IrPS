using System;
using IrpsApi.Framework.Bank;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Bank
{
    [Table("BankAccount", "Bank")]
    public class BankAccount : GeneratedQueryRecord, IBankAccount
    {
        public string AccountId
        {
            get;
            set;
        }

        public string BankId
        {
            get;
            set;
        }

        public string Number
        {
            get;
            set;
        }

        public string Iban
        {
            get;
            set;
        }

        public string BranchName
        {
            get;
            set;
        }

        public string BranchNameEn
        {
            get;
            set;
        }

        public string BranchCode
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

        public bool? DailyPayment
        {
            get;
            set;
        }
    }
}
