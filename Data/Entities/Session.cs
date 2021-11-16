using System;

namespace Group_Guide.Data.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
