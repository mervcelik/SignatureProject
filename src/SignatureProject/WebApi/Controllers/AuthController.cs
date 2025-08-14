using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.ReLogin;
using Application.Features.Auth.Commands.VerifyEmailAuthenticator;
using Application.Features.Users.Queries.GetById;
using Core.Application.Dtos;
using Core.CrossCuttingConcerns.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Application.Features.Auth.Commands.Login.LoggedResponse;

namespace WebApi.Controllers;

public class AuthController : BaseController
{
    private readonly WebApiConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        const string configurationSection = "WebAPIConfiguration";
        _configuration =
            configuration.GetSection(configurationSection).Get<WebApiConfiguration>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        LoggedResponse result = await Mediator.Send(loginCommand);
        return CreateActionResult(ResponseDto<LoggedHttpResponse>.Success(result.ToHttpResponse()));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAddress = getIpAddress() };
        RegisteredResponse result = await Mediator.Send(registerCommand);
        return CreateActionResult(ResponseDto<RegisteredResponse>.Success(result));
    }
    [HttpPost("ReLogin")]
    public async Task<IActionResult> ReLogin([FromBody] ReLoginCommand reLoginCommand)
    {
        reLoginCommand.UserId = getUserIdFromRequest();
        ReLoginResponse result = await Mediator.Send(reLoginCommand);
        return CreateActionResult(ResponseDto<ReLoginResponse>.Success(result));
    }

    [HttpGet("EnableEmailAuthenticator")]
    public async Task<IActionResult> EnableEmailAuthenticator()
    {
        EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand =
            new()
            {
                UserId = getUserIdFromRequest(),
                VerifyEmailUrlPrefix = $"{_configuration.ApiDomain}/Auth/VerifyEmailAuthenticator"
            };
        await Mediator.Send(enableEmailAuthenticatorCommand);
        return CreateActionResult(ResponseDto<NoContentDto>.Success(new NoContentDto()));
    }

    [HttpGet("VerifyEmailAuthenticator")]
    public async Task<IActionResult> VerifyEmailAuthenticator([FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return CreateActionResult(ResponseDto<NoContentDto>.Success(new NoContentDto()));
    }
}