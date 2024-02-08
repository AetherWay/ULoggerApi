namespace LoggerApi.Models
{
    public class GetLogsRequestDTO
    {
        public required string appName;
        public required string environment;
        public DateTime? startDate;
        public DateTime? endDate;
    }
}
