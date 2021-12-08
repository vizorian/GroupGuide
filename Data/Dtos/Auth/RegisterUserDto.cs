using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Auth
{
    public record RegisterUserDto([Required] string Username, [EmailAddress][Required] string Email, [Required] string Password);
}
