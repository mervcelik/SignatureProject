using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CampaignUserRepository : EfRepositoryBase<CampaignUser, int, BaseDbContext>, ICampaignUserRepository
{
    public CampaignUserRepository(BaseDbContext context)
        : base(context) { }
}
