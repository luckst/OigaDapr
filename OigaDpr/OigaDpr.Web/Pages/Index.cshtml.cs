using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OigaDpr.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public IndexModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            try
            {
                var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                    HttpMethod.Get,
                    "registerapi",
                    "WeatherForecast");

                ViewData["WeatherForecastData"] = forecasts;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}