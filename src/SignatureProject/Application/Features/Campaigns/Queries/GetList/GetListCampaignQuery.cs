using Application.Repositories;
using AutoMapper;
using Core.Application.Request;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Campaigns.Queries.GetList;

public class GetListCampaignQuery : IRequest<GetListResponse<GetListCampaignDto>>
{
    public PageRequest PageRequest { get; set; }

    public GetListCampaignQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public GetListCampaignQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }
}

public class GetListCampaignQueryHandler : IRequestHandler<GetListCampaignQuery, GetListResponse<GetListCampaignDto>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IMapper _mapper;
    public GetListCampaignQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
    }
    public async Task<GetListResponse<GetListCampaignDto>> Handle(GetListCampaignQuery request, CancellationToken cancellationToken)
    {
        Paginate<Campaign> campaigns = await _campaignRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        GetListResponse<GetListCampaignDto> response = _mapper.Map<GetListResponse<GetListCampaignDto>>(campaigns);
        return response;
    }
}

