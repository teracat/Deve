namespace Deve
{
    /// <summary>
    /// Common IData definitions accessible from Internal and External apps.
    /// These will have to be implemented in Core, Api & Sdk.
    /// </summary>
    public interface IDataCommon
    {
        DataOptions Options { get; set; }

        IAuthenticate Authenticate { get; }
    }
}
