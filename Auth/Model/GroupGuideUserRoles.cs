using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Auth.Model
{
    public static class GroupGuideUserRoles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, User };
    }
}
