using Application.Features.Auth.Constants;
using Application.Repositories;
using Application.Services.AuthService;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.ReLogin;

public class ReLoginCommand : IRequest<ReLoginResponse>
{
    public int UserId { get; set; }
    public string Token { get; set; }
}

public class ReLoginCommandHandler : IRequestHandler<ReLoginCommand, ReLoginResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;
    public ReLoginCommandHandler(IRefreshTokenRepository refreshTokenRepository, IAuthService authService, IUserRepository userRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<ReLoginResponse> Handle(ReLoginCommand request, CancellationToken cancellationToken)
    {

        var refreshToken = await _refreshTokenRepository.GetAsync(r => r.Token == request.Token && r.UserId == request.UserId, cancellationToken: cancellationToken);
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            throw new Exception(AuthMessages.InvalidRefreshToken);
        }
        var user = await _userRepository.GetAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

        return new ReLoginResponse
        {
            AccessToken = createdAccessToken.Token,
            Token = request.Token,
        };
    }
}