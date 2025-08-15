using Application.Features.Campaigns.Queries.GetList;
using Core.Application.Request;
using Core.CrossCuttingConcerns.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CampaignsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListResponse < GetListCampaignDto > campaigns = await Mediator.Send(new GetListCampaignQuery(pageRequest));
        return CreateActionResult(ResponseDto<GetListResponse<GetListCampaignDto>>.Success(campaigns));
    }
}
