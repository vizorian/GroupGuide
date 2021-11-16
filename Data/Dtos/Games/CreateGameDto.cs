using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Games
{
    public record CreateGameDto([Required] string Name, [Required] string Description);
}
