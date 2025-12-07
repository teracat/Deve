namespace Deve.Internal.Api
{
    public partial class Program
    {
        public static void Main(string[] args) => Deve.Api.ApiBuilder.CreateAndRunApi(args);

        protected Program() { }
    }
}
