using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongodb_dotnet_example.Models
{
    public interface IProductsDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
