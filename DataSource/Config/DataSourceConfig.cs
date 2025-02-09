namespace Deve.DataSource.Config
{
    public class DataSourceConfig
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