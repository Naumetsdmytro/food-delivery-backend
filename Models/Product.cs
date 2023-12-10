using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace mongodb_dotnet_example.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        public string Price { get; set; }
        
        public string Description { get; set; }

        public string Imageurl { get; set; }

        // Add other user-related fields here
    }
}
