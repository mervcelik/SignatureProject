using ApiService.Services;
using Signify.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Extensions;

namespace Signify.WPF.Handler;

public class AuthorizationHandler : DelegatingHandler
{
    private readonly AuthApiService _authApiService;

    public AuthorizationHandler(AuthApiService authApiService)
    {
        _authApiService = authApiService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {

        var token = App.AccessToken;
        if (token == null)
        {
            var accessTokenExpiration = token.IsExpired();
            if (!accessTokenExpiration)
            {
                var newToken = RNH.GetOrThrow(await _authApiService.RefreshTokenAsync());
                if (newToken != null)
                {
                    App.AccessToken = newToken;
                }
            }
        }


        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }

}
