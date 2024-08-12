using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<string> Roles { get; set; }


    }
}
