namespace Deve.DataSource.Config
{
    /// <summary>
    /// DataSource configuration parameters.
    /// </summary>
    public interface IDataSourceConfig
    {
        /// <summary>
        /// ConnectionString used to connect to a Database (it's an example, it's not used in the sample implementation).
        /// </summary>
        string ConnectionString { get; }
    }
}