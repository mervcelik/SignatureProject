using ApiService.Base;
using ApiService.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiService.Registration;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpClient<ApiServiceFactory>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("http://localhost:5293/Api/");
        });
        services.AddTransient<AuthApiService>();
        services.AddTransient<UserApiService>();
        return services;
    }
}
