using Core.Persistence.Repositories;

namespace Domain.Entities;

public class UserCampaign : Entity<int>
{
    public int UserId { get; set; }
    public int CampaignId { get; set; }
    public virtual User? User { get; set; } = null!;
    public virtual Campaign? Campaign { get; set; } = null!;
    public UserCampaign(int userId, int campaignId)
    {
        UserId = userId;
        CampaignId = campaignId;
    }
    public UserCampaign(int id, int userId, int campaignId)
        : base(id)
    {
        UserId = userId;
        CampaignId = campaignId;
    }
}