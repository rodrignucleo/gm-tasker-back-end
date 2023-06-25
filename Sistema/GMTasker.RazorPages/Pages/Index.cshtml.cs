using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages
{
    public class Index : PageModel
    {
        [BindProperty]
        public UsuarioModel UsuarioModel { get; set; } = new();

        public Index(){
        }

        public async Task<IActionResult> OnPostAsync(){

            Console.WriteLine(UsuarioModel.email);
            Console.WriteLine(UsuarioModel.senha);

            var loginModel = new
            {
                email = UsuarioModel.email,
                senha = UsuarioModel.senha
            };

            var httpClient = new HttpClient();
            var url = $"http://localhost:5072/api/Login/{UsuarioModel.email}/{UsuarioModel.senha}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                TempData["SenhaErrada"] = "Email ou Senha errada!";
                return RedirectToPage('/');
            }
            
            return RedirectToPage("/IndexOld");
        }
        
    }
}