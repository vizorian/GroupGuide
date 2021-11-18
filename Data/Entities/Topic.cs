using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
