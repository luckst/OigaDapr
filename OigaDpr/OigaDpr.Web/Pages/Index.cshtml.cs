using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OigaDpr.Web.Models;

namespace OigaDpr.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DaprClient _daprClient;
        public PaginatedUserList List { get; set; }
        public string Filter { get; set; }

        public IndexModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet(string filter, string searchString, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = filter;
            }

            Filter = searchString;

            var result = await _daprClient.InvokeMethodAsync<PaginatedUserList>(
                HttpMethod.Get,
                "searchapi",
                $"api/search?filters={searchString ?? ""}&pageNumber={pageIndex ?? 1}");

            List = result;
        }
    }
}