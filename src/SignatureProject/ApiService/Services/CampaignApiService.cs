using ApiService.Base;
using Application.Features.Campaigns.Queries.GetList;
using Core.Application.Request;
using Core.CrossCuttingConcerns.Dtos;
using Core.CrossCuttingConcerns.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiService.Services;

public class CampaignApiService : BaseApiService
{
    public CampaignApiService(ApiServiceFactory apiServiceFactory) : base(apiServiceFactory)
    {
    }

    public async Task<ResponseDto<GetListResponse<GetListCampaignDto>>> GetList(PageRequest pageRequest)
    {
        var response = await _httpClient.GetAsync(pageRequest.ToQueryString("Campaigns"));
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<GetListResponse<GetListCampaignDto>>>();
        return responsebody;
    }
}
