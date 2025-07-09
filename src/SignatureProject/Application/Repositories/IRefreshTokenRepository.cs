using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken, int>, IRepository<RefreshToken, int>
{
    Task<List<RefreshToken>> GetOldRefreshTokensAsync(int userID, int refreshTokenTTL);
}
