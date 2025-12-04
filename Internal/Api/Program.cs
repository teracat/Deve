namespace Deve.Internal.Api
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            new Deve.Api.ApiBuilder().CreateAndRunApi(args);
        }

        protected Program() {}
    }
}
