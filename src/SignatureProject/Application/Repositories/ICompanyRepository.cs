using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories;

public interface ICompanyRepository : IAsyncRepository<Company, int>, IRepository<Company, int> { }
