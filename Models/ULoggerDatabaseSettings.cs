namespace LoggerApi.Models
{
    public class ULoggerDatabaseSettings
    {        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CustomFieldsCollectionName { get; set; } = null!;
        public string LogsCollectionName { get; set; } = null!;
    }
}
