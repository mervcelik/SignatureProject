using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories;

public interface IOtpAuthenticatorRepository : IAsyncRepository<OtpAuthenticator, int>, IRepository<OtpAuthenticator, int> { }