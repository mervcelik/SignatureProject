using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.AuthenticatorService;
using Core.Application.Piplines.Authorization;
using Core.Mailing.Abstraction;
using Core.Mailing.Dto;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest, ISecuredRequest
{
    public int UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public string[] Roles => [];

    public EnableEmailAuthenticatorCommand()
    {
        VerifyEmailUrlPrefix = string.Empty;
    }

    public EnableEmailAuthenticatorCommand(int userId, string verifyEmailUrlPrefix)
    {
        UserId = userId;
        VerifyEmailUrlPrefix = verifyEmailUrlPrefix;
    }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;

        public EnableEmailAuthenticatorCommandHandler(
            IUserRepository userRepository,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            IMailService mailService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userRepository = userRepository;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Id == request.UserId,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            user!.AuthenticatorType = AuthenticatorType.Email;
            await _userRepository.UpdateAsync(user);

            EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);
            EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            var toEmailList = new List<MailboxAddress> { new(name: user.Email, user.Email) };
            var url= request.VerifyEmailUrlPrefix +"/"+addedEmailAuthenticator.ActivationKey;
            var htmlBody = string.Format(
              "\r\n<div style=\" font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            color: #333;\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            padding: 20px;\r\n            text-align: center;\"> \r\n    <div style=\" color: #414242;\r\n            font-size: 24px;\r\n            margin-bottom: 20px;\r\n            font-weight: bold;\">\r\n            Welcome to Signify\r\n    </div>\r\n    \r\n    <p>Please click the link below to verify your email address.</p>\r\n    \r\n    <div style=\"border-top: 1px solid #e0e0e0;\r\n            margin: 20px 0;\"></div>\r\n    <a href=\"{0}\" style=\" display: inline-block;\r\n            background-color: #414242;\r\n            color: white;\r\n            padding: 10px 20px;\r\n            text-decoration: none;\r\n            border-radius: 4px;\r\n            font-weight: bold;\r\n            margin: 15px 0;\">\r\n            Verify Email Address\r\n    </a>\r\n    \r\n    <div style=\"border-top: 1px solid #e0e0e0; margin: 20px 0;\"></div>\r\n    \r\n    <div style=\"margin-top: 30px;\r\n            font-style: italic;\r\n            color: #666;\">\r\n        All the best,<br>\r\n        Signify\r\n    </div>\r\n</div>\r\n", url
            );
            _mailService.SendMail(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Verify Your Email - Signify",
                    HtmlBody = htmlBody,
                }
            );
        }
    }
}