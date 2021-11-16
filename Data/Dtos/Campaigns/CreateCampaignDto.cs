using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Campaigns
{
    public record CreateCampaignDto([Required] string Name, [Required] string Description);
}
