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

        public Login(){
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
                return Page();
            }

            var usuariosJson = await response.Content.ReadAsStringAsync();
            var usuarios = JsonConvert.DeserializeObject<UsuarioModel>(usuariosJson);

            return Redirect($"/Usuario/Details/{usuarios!.id_usuario}");
        }
        
    }
}