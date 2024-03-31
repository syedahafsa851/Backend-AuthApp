using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AuthApp.DBModel
{
    public class UserDBModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("email")]
        public string? Email { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }
    }
}
