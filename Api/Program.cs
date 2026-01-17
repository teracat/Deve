namespace Deve.Api;

public class Program
{
    public static void Main(string[] args) =>
        ApiBuilder.Create(args)
                  .Configure()
                  .AddModules()
                  .Build()
                  .MapEndpointsModules()
                  .Run();

    protected Program() { }
}
