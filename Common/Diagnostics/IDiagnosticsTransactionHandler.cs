namespace Deve.Diagnostics
{
    public interface IDiagnosticsTransactionHandler : IDisposable
    {
        /// <summary>
        /// Starts a new transaction.
        /// </summary>
        /// <param name="name">The name of the transaction.</param>
        /// <param name="operation">The operation name of the transaction.</param>
        void StartTransaction(string name, string operation);

        /// <summary>
        /// Stops the last transaction.
        /// </summary>
        void StopTransaction();
    }
}
