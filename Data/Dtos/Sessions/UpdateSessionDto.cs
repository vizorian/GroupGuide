using System;
using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Sessions
{
    public record UpdateSessionDto([Required] string Name, [Required] DateTime StartingTime);
}
