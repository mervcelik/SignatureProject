using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories;

public interface ICampaignUserRepository : IAsyncRepository<CampaignUser, int>, IRepository<CampaignUser, int> { }
