using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiService.Base;

public class BaseApiService
{
    internal ApiServiceFactory _apiServiceFactory;
    internal HttpClient _httpClient => _apiServiceFactory._httpClient;

    public BaseApiService(ApiServiceFactory apiServiceFactory)
    {
        _apiServiceFactory = apiServiceFactory;
    }
}
