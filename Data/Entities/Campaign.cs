using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Entities
{
    public class Campaign : IUserOwnedResource, IUserBelongableResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public string UserId { get; set; }
        public GroupGuideUser User { get; set; }

        public ICollection<string> PlayerIds { get; set; }
    }
}
