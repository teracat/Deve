using Deve.Data;

namespace Deve.Sdk;

public static class SdkBuilder
{
    public static ISdk Create(EnvironmentType environment, IDataOptions options, HttpMessageHandler handler) =>
        new MainSdk(environment, options, handler);

    public static ISdk Create(EnvironmentType environment, HttpMessageHandler handler) =>
        new MainSdk(environment, new DataOptions(), handler);

    public static ISdk Create(EnvironmentType environment, IDataOptions options) =>
        new MainSdk(environment, options);

    public static ISdk Create(EnvironmentType environment) =>
        new MainSdk(environment, new DataOptions());

    public static ISdk Create(HttpClient client, DataOptions options) =>
        new MainSdk(client, options);

    public static ISdk Create(HttpClient client) =>
        new MainSdk(client, new DataOptions());
}
