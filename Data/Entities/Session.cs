using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Entities
{
    public class Session : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }

        public string UserId { get; set; }
        public GroupGuideUser User { get; set; }
    }
}
