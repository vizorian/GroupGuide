using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Entities
{
    public class Post : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        
        public string UserId { get; set; }
    }
}
