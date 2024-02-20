using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace LoggerApi.Models
{
    public class Log
    {
        public Log(string appName, string environment, LogLevel logLevel, string message)
        {
            AppName = appName;
            Environment = environment;
            LogLevel = ((short)logLevel);
            Message = message;
            CreatedWhen = DateTime.UtcNow;
        }

        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; }

        public string AppName { get; set; }

        public string Environment { get; set; }

        public int LogLevel { get; set; }

        public string Message { get; set; }

        public DateTime? CreatedWhen { get; }

        public Dictionary<string, JsonObject>? CustomFields { get; set; }
    }
}
