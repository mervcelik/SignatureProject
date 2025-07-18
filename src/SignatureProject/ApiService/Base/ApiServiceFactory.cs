using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiService.Base;

public class ApiServiceFactory
{
    internal HttpClient _httpClient;

    public ApiServiceFactory(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
