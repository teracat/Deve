using Deve.Logging;
using System.Transactions;

namespace Deve.Diagnostics
{
    /// <summary>
    /// Manages the lifecycle of a Sentry transaction for tracking user sessions in a MAUI application.
    /// </summary>
    public class TransactionHandlerSentry : IDiagnosticsTransactionHandler
    {
        private readonly List<ITransactionTracer> _transactions = [];

        /// <summary>
        /// Starts a new transaction.
        /// </summary>
        /// <param name="name">The name of the transaction.</param>
        /// <param name="operation">The operation name.</param>
        public void StartTransaction(string name, string operation)
        {
            Log.Debug("TransactionHandlerSentry - Starting transaction: {Name}, Operation: {Operation}", name, operation);

            lock (_transactions)
            {
                var transaction = SentrySdk.StartTransaction(name, operation);
                SentrySdk.ConfigureScope(scope => scope.Transaction = transaction);
                _transactions.Add(transaction);
            }
        }

        /// <summary>
        /// Stops the last transaction.
        /// </summary>
        public void StopTransaction()
        {
            Log.Debug("TransactionHandlerSentry - Stopping transaction");

            lock (_transactions)
            {
                if (_transactions.Count == 0)
                    return;

                var transaction = _transactions.Last();
                transaction.Finish();

                _transactions.RemoveAt(_transactions.Count - 1);

                var previousTransaction = _transactions.LastOrDefault();
                SentrySdk.ConfigureScope(scope => scope.Transaction = previousTransaction);
            }
        }

        /// <summary>
        /// Stops all the transactions.
        /// </summary>
        private void StopAllTransactions()
        {
            Log.Debug("TransactionHandlerSentry - Stopping all transactions");

            lock (_transactions)
            {
                for (int i = _transactions.Count - 1; i >= 0; i--)
                {
                    var transaction = _transactions[i];
                    transaction.Finish();
                }
                _transactions.Clear();

                SentrySdk.ConfigureScope(scope => scope.Transaction = null);
            }
        }

        #region IDisposable
        public void Dispose() => StopAllTransactions();
        #endregion
    }
}
