using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OigaDpr.Web.Models;

namespace OigaDpr.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly DaprClient _daprClient;
        public User UserModel { get; set; }
        public string ErrorMessage { get; set; }

        public DetailsModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }


        public async Task OnGet(string username)
        {
            try
            {
                UserModel = await _daprClient.InvokeMethodAsync<User>(
                       HttpMethod.Get,
                       "searchapi",
                       $"api/Search/{username}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
