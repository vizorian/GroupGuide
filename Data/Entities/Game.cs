using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Entities
{
    public class Game : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }

        [Required]
        public string UserId { get; set; }
        public GroupGuideUser User { get; set; }
    }
}
