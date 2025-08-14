using Core.Persistence.Repositories;

namespace Domain.Entities;

public class CompanyUser : Entity<int>
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
