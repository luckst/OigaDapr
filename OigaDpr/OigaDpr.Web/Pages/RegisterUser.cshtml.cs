using Dapr.Client;
using Google.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OigaDpr.Web.Models;

namespace OigaDpr.Web.Pages
{
    public class RegisterUserModel : PageModel
    {
        private readonly DaprClient _daprClient;

        [BindProperty]
        public User UserModel { get; set; }
        public string ErrorMessage { get; set; }

        public RegisterUserModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _daprClient.InvokeMethodAsync(
                       HttpMethod.Post,
                       "registerapi",
                       "api/Register",
                       UserModel);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
