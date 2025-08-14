using Core.Persistence.Repositories;

namespace Domain.Entities;

public class CampaignUser : Entity<int>
{
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
