using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Posts
{
    public record CreatePostDto([Required] string Content);
}
