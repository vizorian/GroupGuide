using Group_Guide.Data.Dtos.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_Guide.Data.Entities
{
    public class CampaignUser
    {
        //[Key, Column(Order = 1)]
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        //[Key, Column(Order = 2)]
        public int UserId { get; set; }
        public GroupGuideUser User { get; set; }
    }
}
