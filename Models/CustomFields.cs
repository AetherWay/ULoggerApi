using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace LoggerApi.Models
{
    public class CustomFields
    {
        public CustomFields(string appName, string environment)
        {
            AppName = appName;
            Environment = environment;
        }

        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string AppName { get; set; }

        public string Environment { get; set; }

        public Dictionary<string, string>? Fields { get; set; }
    }
}
