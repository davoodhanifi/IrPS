using System;

namespace IrpsApi.Framework.Bank
{
    public interface IBankAccount : IRecord
    {
        string AccountId
        {
            get;
            set;
        }

        string BankId
        {
            get;
            set;
        }

        string Number
        {
            get;
            set;
        }

        string Iban
        {
            get;
            set;
        }

        string BranchName
        {
            get;
            set;
        }

        string BranchNameEn
        {
            get;
            set;
        }

        string BranchCode
        {
            get;
            set;
        }

        string TypeId
        {
            get;
            set;
        }

        bool? DailyPayment
        {
            get;
            set;
        }
    }
}
