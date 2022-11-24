using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OigaDpr.Web.Models;

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
            string filters = "";

            var users = await _daprClient.InvokeMethodAsync<IEnumerable<User>>(
                HttpMethod.Get,
                "searchapi",
                $"api/search?filters={filters}");

            ViewData["WeatherForecastData"] = users;
        }
    }
}