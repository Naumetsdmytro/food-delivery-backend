using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace mongodb_dotnet_example.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        // Add other user-related fields here
    }
}
