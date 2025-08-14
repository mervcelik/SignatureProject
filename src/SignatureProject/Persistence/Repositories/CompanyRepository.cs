using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CompanyRepository : EfRepositoryBase<Company, int, BaseDbContext>, ICompanyRepository
{
    public CompanyRepository(BaseDbContext context)
        : base(context) { }
}
