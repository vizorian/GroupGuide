using System.Collections.Generic;
using Group_Guide.Data.Dtos.Auth;

namespace Group_Guide.Data.Dtos.Campaigns
{
    public record CampaignDto(int Id, string Name, string Description, List<GroupGuideUser> Players);
}
