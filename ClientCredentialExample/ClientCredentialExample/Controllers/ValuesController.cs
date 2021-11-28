using ClientCredentialExample.Models;
using ClientCredentialExample.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClientCredentialExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAuthServerService _authServerService;
        private readonly IHttpClientFactory _httpClient;
        public ValuesController(IAuthServerService authServerService, IHttpClientFactory httpClient)
        {
            _authServerService = authServerService;
            _httpClient = httpClient;
        }
        public async Task<List<WeatherForecast>> Get() 
        {
            var tokenResponse = await _authServerService.RequestClientCredentialsTokenAsync();
            if(tokenResponse != null)
            {
                var apiClient = _httpClient.CreateClient();
                apiClient.SetBearerToken(tokenResponse);
                var response = await apiClient.GetAsync("http://localhost:1000/api/WeatherForecast");
                var content = await response.Content.ReadAsStringAsync();
                apiClient.Dispose();
                if (response.IsSuccessStatusCode)
                {
                    var res = JsonSerializer.Deserialize<List<WeatherForecast>>(content);
                    return res;
                }
            }
            
            return new List<WeatherForecast>(); 
        }
    }
}
