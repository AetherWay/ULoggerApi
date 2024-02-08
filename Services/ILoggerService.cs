using LoggerApi.Models;

namespace LoggerApi.Services
{
    public interface ILoggerService
    {
        public Task<CustomFields> GetCustomFieldsAsync(string id);

        public Task<string?> GetCustomFieldsIdByAppName(string appName, string environment);

        public Task<List<Log>> GetLogsAsync(string appName, string environment, DateTime? startDateTime = null, DateTime? endDateTime = null);

        public Task CreateCustomFieldsAsync(CustomFields customFields);

        public Task UpdateCustomFieldsAsync(string id, CustomFields customFields);

        public Task DeleteCustomFieldsAsync(string id);

        public Task CreateLogAsync(Log log);
    }
}
