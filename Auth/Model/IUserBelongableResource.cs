using Group_Guide.Data.Dtos.Auth;
using System.Collections.Generic;

namespace Group_Guide.Auth.Model
{
    public interface IUserBelongableResource
    {
        ICollection<GroupGuideUser> Players { get; set; }
    }
}