using Application.Repositories;
using AutoMapper;
using Core.Security.JWT;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly TokenOptions _tokenOptions;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IMapper _mapper;

    public AuthManager(
        IUserOperationClaimRepository userOperationClaimRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenHelper tokenHelper,
        IConfiguration configuration,
        IMapper mapper
    )
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;

        const string tokenOptionsConfigurationSection = "TokenOptions";
        _tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration");
        _mapper = mapper;
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {
        IList<OperationClaim> operationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(user.Id);

        IList<Core.Security.Entities.OperationClaim> lst = _mapper.Map<IList<Core.Security.Entities.OperationClaim>>(operationClaims);
        var userEntity = new Core.Security.Entities.User
        {
            AuthenticatorType = (Core.Security.Enums.AuthenticatorType)user.AuthenticatorType,
            CreatedDate = user.CreatedDate,
            DeletedDate = user.DeletedDate,
            Email = user.Email,
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
            UpdatedDate = user.UpdatedDate,
            Status = user.Status,
        };
        AccessToken accessToken = _tokenHelper.CreateToken(userEntity, lst);
        return accessToken;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(int userId)
    {
        List<RefreshToken> refreshTokens = await _refreshTokenRepository.GetOldRefreshTokensAsync(userId, _tokenOptions.RefreshTokenTTL);
        await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);
    }

    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == token);
        return refreshToken;
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        var userEntity = new Core.Security.Entities.User
        {
            AuthenticatorType = (Core.Security.Enums.AuthenticatorType)user.AuthenticatorType,
            CreatedDate = user.CreatedDate,
            DeletedDate = user.DeletedDate,
            Email = user.Email,
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
            UpdatedDate = user.UpdatedDate,
            Status = user.Status,
        };
        Core.Security.Entities.RefreshToken coreRefreshToken = _tokenHelper.CreateRefreshToken(userEntity, ipAddress);
        RefreshToken refreshToken = _mapper.Map<RefreshToken>(coreRefreshToken);
        return Task.FromResult(refreshToken);
    }
}