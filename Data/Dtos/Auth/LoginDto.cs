using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Dtos.Auth
{
    public record LoginDto(string UserName, string Password);
}
