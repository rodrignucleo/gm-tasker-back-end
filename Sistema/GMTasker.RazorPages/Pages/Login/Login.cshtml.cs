using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Login
{
    public class Login : PageModel
    {
        [BindProperty]
        public UsuarioModel UsuarioModel { get; set; } = new();

        public Login()
        {
            
        }
        public async Task<IActionResult> OnPosttAsync(){
            if(!ModelState.IsValid){
                return Page();
            }
            
            var httpClient = new HttpClient();
            var url = $"http://localhost:5072/api/Login/";
            var loginJson = JsonConvert.SerializeObject(UsuarioModel);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(loginJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return Page();
            }

            return Redirect("/Index");
        }
        
    }
}