using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr.Client;

namespace DaprFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(DaprClient daprClient, ILogger<IndexModel> logger)
        {
            _daprClient = daprClient ?? throw new ArgumentException(nameof(daprClient));
            _logger = logger;
        }

        public async Task OnGet()
        {
            var forecasts =
                await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "daprbackend",
                    "weatherforecast");
            ViewData["WeatherForecastData"] = forecasts;
        }
    }
}
