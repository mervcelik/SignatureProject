﻿using Core.Persistence.Repositories;
using Domain.Entities;


namespace Application.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim, int>, IRepository<OperationClaim, int> { }