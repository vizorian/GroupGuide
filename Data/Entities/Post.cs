using System;

namespace Group_Guide.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
