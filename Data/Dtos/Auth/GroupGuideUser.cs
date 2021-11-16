using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Dtos.Auth
{
    public class GroupGuideUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string AdditionalInfo { get; set; }
    }
}
