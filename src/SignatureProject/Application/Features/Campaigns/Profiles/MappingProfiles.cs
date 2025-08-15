using Application.Features.Campaigns.Queries.GetList;
using AutoMapper;
using Core.Application.Request;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Campaigns.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Campaign, GetListCampaignDto>().ReverseMap();
        CreateMap<Paginate<Campaign>, GetListResponse<GetListCampaignDto>>().ReverseMap();
    }
}
