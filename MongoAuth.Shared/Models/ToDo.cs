using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoAuth.Shared.Models
{
    public class ToDo
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("Title")]
        public string? Title { get; set; } 

        [BsonElement("TaskAddedDate")]
        public DateOnly? TaskAddedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        [BsonElement("Completed")]
        public bool Completed { get; set; } = false;
        
        [BsonElement("TaskCompletedDate")]
        public DateOnly? TaskCompletedDate { get; set; } = null;

    }

}