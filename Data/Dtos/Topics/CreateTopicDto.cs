using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Topics
{
    public record CreateTopicDto([Required] string Name);
}
