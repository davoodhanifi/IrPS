using System;

namespace IrpsApi.Framework.Accounting
{
    public enum TransactionType
    {
        None = 0,
        Payment = 1,
        IncreaseCredit = 2,
        Reward = 3,
    }
}
