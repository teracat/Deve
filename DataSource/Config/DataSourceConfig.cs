namespace Deve.DataSource.Config
{
    /// <summary>
    /// DataSource configuration parameters.
    /// </summary>
    public class DataSourceConfig : IDataSourceConfig
    {
        public string ConnectionString { get; set; }

        public DataSourceConfig()
        {
            ConnectionString = string.Empty;
        }

        public DataSourceConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}