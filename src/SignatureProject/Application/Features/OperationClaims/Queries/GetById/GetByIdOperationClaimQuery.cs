﻿using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Application.Piplines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery : IRequest<GetByIdOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [OperationClaimsOperationClaims.Read];

    public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, GetByIdOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public GetByIdOperationClaimQueryHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<GetByIdOperationClaimResponse> Handle(
            GetByIdOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken,
                enableTracking: false
            );
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            GetByIdOperationClaimResponse response = _mapper.Map<GetByIdOperationClaimResponse>(operationClaim);
            return response;
        }
    }
}
