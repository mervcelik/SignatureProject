using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories;

public interface IEmailAuthenticatorRepository : IAsyncRepository<EmailAuthenticator, int>, IRepository<EmailAuthenticator, int> { }