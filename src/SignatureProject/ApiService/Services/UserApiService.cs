using ApiService.Base;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
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

public class UserApiService : BaseApiService
{
    public UserApiService(ApiServiceFactory apiServiceFactory) : base(apiServiceFactory)
    {

    }

    public async Task<ResponseDto<GetByIdUserResponse>> GetByIdAsync(GetByIdUserQuery getByIdUserQuery)
    {
        var response = await _httpClient.GetAsync($"Users/{getByIdUserQuery.Id}");
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<GetByIdUserResponse>>();
        return responsebody;
    }

    public async Task<ResponseDto<GetByIdUserResponse>> GetFromAuthAsync()
    {
        var response = await _httpClient.GetAsync("Users/GetFromAuth");
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<GetByIdUserResponse>>();
        return responsebody;
    }
    public async Task<ResponseDto<GetListResponse<GetListUserListItemDto>>> GetListAsync(PageRequest pageRequest)
    {
        var response = await _httpClient.GetAsync(pageRequest.ToQueryString("User"));
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<GetListResponse<GetListUserListItemDto>>>();
        return responsebody;
    }
}
