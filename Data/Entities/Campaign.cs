using System;

namespace Group_Guide.Data.Entities
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
