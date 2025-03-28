using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongodb_MVC.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Status { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = string.Empty;
    }

}
