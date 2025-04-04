﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongodb_MVC.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } 
        public DateTime? CreateDate { get; set; }
    }

}
