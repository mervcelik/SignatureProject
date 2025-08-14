using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories;

public interface ICompanyUserRepository : IAsyncRepository<CompanyUser, int>, IRepository<CompanyUser, int> { }
