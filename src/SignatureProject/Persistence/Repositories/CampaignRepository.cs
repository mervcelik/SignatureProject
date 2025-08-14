using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CampaignRepository : EfRepositoryBase<Campaign, int, BaseDbContext>, ICampaignRepository
{
    public CampaignRepository(BaseDbContext context)
        : base(context) { }
}
