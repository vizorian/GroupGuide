using System.ComponentModel.DataAnnotations;

namespace Group_Guide.Data.Dtos.Campaigns
{
    public record UpdateCampaignDto([Required] string Name, [Required] string Description);
}
