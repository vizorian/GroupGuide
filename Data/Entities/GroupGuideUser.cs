using Group_Guide.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Group_Guide.Data.Dtos.Auth
{
    public class GroupGuideUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<Campaign> Campaigns { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
