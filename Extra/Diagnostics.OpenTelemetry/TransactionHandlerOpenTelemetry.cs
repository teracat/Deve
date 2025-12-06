using Deve.Logging;
using OpenTelemetry.Trace;

namespace Deve.Diagnostics
{
    /// <summary>
    /// Handles OpenTelemetry transactions (spans).
    /// </summary>
    public class TransactionHandlerOpenTelemetry : IDiagnosticsTransactionHandler
    {
        private readonly TracerProvider _tracerProvider;
        private readonly List<TelemetrySpan> _spans = [];

        public TransactionHandlerOpenTelemetry(TracerProvider tracerProvider)
        {
            _tracerProvider = tracerProvider;
        }

        /// <summary>
        /// Starts a new transaction.
        /// </summary>
        /// <param name="name">The name of the span</param>
        /// <param name="operation">The operation name</param>
        public void StartTransaction(string name, string operation)
        {
            Log.Debug("TransactionHandlerOpenTelemetry - Starting span: {Name}, Operation: {Operation}", name, operation);

            lock (_spans)
            {
                var tracer = _tracerProvider.GetTracer(operation);
                var span = tracer?.StartSpan(name);
                if (span is not null)
                {
                    _spans.Add(span);
                }
            }
        }

        /// <summary>
        /// Stops the last transaction.
        /// </summary>
        public void StopTransaction()
        {
            Log.Debug("TransactionHandlerOpenTelemetry - Stopping span");

            lock (_spans)
            {
                if (_spans.Count == 0)
                {
                    return;
                }

                var span = _spans.Last();
                span.Dispose();

                _spans.RemoveAt(_spans.Count - 1);
            }
        }

        /// <summary>
        /// Stops all the transactions.
        /// </summary>
        private void StopAllTransactions()
        {
            Log.Debug("TransactionHandlerOpenTelemetry - Stopping all spans");

            lock (_spans)
            {
                for (int i = _spans.Count - 1; i >= 0; i--)
                {
                    var span = _spans[i];
                    span.Dispose();
                }
                _spans.Clear();
            }
        }

        #region IDisposable
        public void Dispose() => StopAllTransactions();
        #endregion
    }
}
