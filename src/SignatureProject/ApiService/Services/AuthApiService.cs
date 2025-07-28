using ApiService.Base;
using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticator;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Commands.VerifyEmailAuthenticator;
using Application.Features.Auth.Commands.VerifyOtpAuthenticator;
using Core.Application.Dtos;
using Core.CrossCuttingConcerns.Dtos;
using Core.CrossCuttingConcerns.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Auth.Commands.Login.LoggedResponse;

namespace ApiService.Services;

public class AuthApiService : BaseApiService
{
    public AuthApiService(ApiServiceFactory apiServiceFactory) : base(apiServiceFactory)
    {
    }

    public async Task<ResponseDto<LoggedHttpResponse>> LoginAsync(LoginCommand loginCommand)
    {
        var response = await _httpClient.PostAsJsonAsync("Auth/Login", loginCommand);
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<LoggedHttpResponse>>();
        return responsebody;
    }

    public async Task<ResponseDto<string>> RegisterAsync(RegisterCommand registerCommand)
    {
        var response = await _httpClient.PostAsJsonAsync("Auth/Register", registerCommand);
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<string>>();
        return responsebody;
    }

    public async Task<ResponseDto<string>> RefreshTokenAsync()
    {
        var response = await _httpClient.GetAsync("Auth/RefreshToken");
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<string>>();
        return responsebody;
    }

    public async Task<ResponseDto<RevokedTokenResponse>> RevokeTokenAsync(RevokeTokenCommand? revokeTokenCommand)
    {
        var response = await _httpClient.PutAsJsonAsync("Auth/RevokeToken", revokeTokenCommand);
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<RevokedTokenResponse>>();
        return responsebody;
    }

    public async Task<ResponseDto<object>> EnableEmailAuthenticatorAsync()
    {
        var response = await _httpClient.GetAsync("Auth/EnableEmailAuthenticator");
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<object>>();
        return responsebody;
    }

    public async Task<ResponseDto<EnabledOtpAuthenticatorResponse>> EnableOtpAuthenticatorAsync()
    {
        var response = await _httpClient.GetAsync("Auth/EnableOtpAuthenticator");
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<EnabledOtpAuthenticatorResponse>>();
        return responsebody;
    }

    public async Task<ResponseDto<object>> VerifyEmailAuthenticatorAsync(VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        var response = await _httpClient.GetAsync($"Auth/VerifyEmailAuthenticator?{verifyEmailAuthenticatorCommand.ToQueryString()}");
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<object>>();
        return responsebody;
    }

    public async Task<ResponseDto<object>> VerifyOtpAuthenticatorAsync(VerifyOtpAuthenticatorCommand verifyOtpAuthenticatorCommand)
    {
        var response = await _httpClient.PostAsJsonAsync("Auth/VerifyOtpAuthenticator", verifyOtpAuthenticatorCommand);
        var responsebody = await response.Content.ReadFromJsonAsync<ResponseDto<object>>();
        return responsebody;
    }

}
