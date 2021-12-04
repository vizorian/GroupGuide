using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_Guide.Data.Entities
{
    public class Campaign : IUserBelongableResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<GroupGuideUser> Players { get; set; }
    }
}
