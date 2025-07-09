using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.AuthenticatorService;
using Core.Application.Piplines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticator;

public class EnableOtpAuthenticatorCommand : IRequest<EnabledOtpAuthenticatorResponse>, ISecuredRequest
{
    public int UserId { get; set; }

    public string[] Roles => [];

    public class EnableOtpAuthenticatorCommandHandler
        : IRequestHandler<EnableOtpAuthenticatorCommand, EnabledOtpAuthenticatorResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IUserRepository _userRepository;

        public EnableOtpAuthenticatorCommandHandler(
            IUserRepository userRepository,
            IOtpAuthenticatorRepository otpAuthenticatorRepository,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userRepository = userRepository;
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<EnabledOtpAuthenticatorResponse> Handle(
            EnableOtpAuthenticatorCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Id == request.UserId,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            OtpAuthenticator? doesExistOtpAuthenticator = await _otpAuthenticatorRepository.GetAsync(
                predicate: o => o.UserId == request.UserId,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.OtpAuthenticatorThatVerifiedShouldNotBeExists(doesExistOtpAuthenticator);
            if (doesExistOtpAuthenticator is not null)
                await _otpAuthenticatorRepository.DeleteAsync(doesExistOtpAuthenticator);

            OtpAuthenticator newOtpAuthenticator = await _authenticatorService.CreateOtpAuthenticator(user!);
            OtpAuthenticator addedOtpAuthenticator = await _otpAuthenticatorRepository.AddAsync(newOtpAuthenticator);

            EnabledOtpAuthenticatorResponse enabledOtpAuthenticatorDto =
                new() { SecretKey = await _authenticatorService.ConvertSecretKeyToString(addedOtpAuthenticator.SecretKey) };
            return enabledOtpAuthenticatorDto;
        }
    }
}
