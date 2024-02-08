using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LoggerApi.Models
{
    public class Log
    {
        public Log(string appName, string environment, LogLevel logLevel, string message)
        {
            AppName = appName;
            Environment = environment;
            LogLevel = logLevel;
            Message = message;
            CreatedWhen = DateTime.UtcNow;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; }

        public string AppName { get; set; }

        public string Environment { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Message { get; set; }

        public DateTime? CreatedWhen { get; }

        public List<object>? CustomFields { get; set; }
    }
}
