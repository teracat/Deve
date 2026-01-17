namespace Deve.Sdk.LoggingHandlers;

/// <summary>
/// Base class to process HTTP requests and responses.
/// </summary>
public abstract class LoggingHandlerBase : DelegatingHandler
{
    private readonly string _outputPrefix = string.Empty;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed in DelegatingHandler")]
    protected LoggingHandlerBase()
        : base(new HttpClientHandler())
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed in DelegatingHandler")]
    protected LoggingHandlerBase(string outputPrefix)
        : base(new HttpClientHandler())
    {
        _outputPrefix = outputPrefix;
    }

    protected LoggingHandlerBase(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected LoggingHandlerBase(HttpMessageHandler innerHandler, string outputPrefix)
        : base(innerHandler)
    {
        _outputPrefix = outputPrefix;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await ProcessRequest(request, cancellationToken);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        await ProcessResponse(response, cancellationToken);

        return response;
    }

    protected virtual async Task ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string msg = request.ToString();
        string content = string.Empty;
        if (request.Content is not null)
        {
            content = await request.Content.ReadAsStringAsync(cancellationToken);
        }

        WriteRequest(msg, content);
    }

    protected virtual async Task ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(response);

        string msg = response.ToString();
        string content = string.Empty;
        if (response.Content is not null)
        {
            content = await response.Content.ReadAsStringAsync(cancellationToken);
        }

        WriteResponse(msg, content);
    }

    protected virtual void WriteRequest(string message, string content)
    {
        Write(_outputPrefix + "Request:");
        Write(_outputPrefix + message);
        if (!string.IsNullOrEmpty(content))
        {
            Write(_outputPrefix + content);
        }
    }

    protected virtual void WriteResponse(string message, string content)
    {
        Write(_outputPrefix + "Response:");
        Write(_outputPrefix + message);
        if (!string.IsNullOrEmpty(content))
        {
            Write(_outputPrefix + content);
        }
    }

    protected abstract void Write(string text);
}
