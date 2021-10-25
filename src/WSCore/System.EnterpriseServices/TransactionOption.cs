using System;

namespace System.EnterpriseServices
{
    //
    // Summary:
    //     Specifies the automatic transaction type requested by the component.
    public enum TransactionOption
    {
        //
        // Summary:
        //     Ignores any transaction in the current context.
        Disabled = 0,
        //
        // Summary:
        //     Creates the component in a context with no governing transaction.
        NotSupported = 1,
        //
        // Summary:
        //     Shares a transaction, if one exists.
        Supported = 2,
        //
        // Summary:
        //     Shares a transaction, if one exists, and creates a new transaction if necessary.
        Required = 3,
        //
        // Summary:
        //     Creates the component with a new transaction, regardless of the state of the
        //     current context.
        RequiresNew = 4
    }

    public delegate void TransactedCallback();

    internal class Transactions
    {
        public static void InvokeTransacted(TransactedCallback callback, TransactionOption option)
        {
            throw new NotImplementedException();
        }
    }
}
