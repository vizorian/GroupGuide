using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Posts
{
    public record UpdatePostDto([Required] string Content);
}
