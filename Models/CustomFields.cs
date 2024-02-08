using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LoggerApi.Models
{
    public class CustomFields
    {
        public CustomFields(string appName, string environment)
        {
            AppName = appName;
            Environment = environment;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string AppName { get; set; }

        public string Environment { get; set; }

        public List<object>? Fields { get; }
    }
}
