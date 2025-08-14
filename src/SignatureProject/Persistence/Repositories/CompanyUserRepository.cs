using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CompanyUserRepository : EfRepositoryBase<CompanyUser, int, BaseDbContext>, ICompanyUserRepository
{
    public CompanyUserRepository(BaseDbContext context)
        : base(context) { }
}
