using System.Text;
using GMTasker.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GMTasker.RazorPages.Pages
{
    public class IndexOld : PageModel
    {
        [BindProperty]
        public UsuarioModel UsuarioModel { get; set; } = new();
        public IndexOld(){
        }
        
        public async Task<IActionResult> OnGetAsync(int id){

            if(id == 0){
                var httpClientSession = new HttpClient();
                var urlSession = $"http://localhost:5072/api/Login/currentUser";
                var requestMessageSession = new HttpRequestMessage(HttpMethod.Get, urlSession);
                // requestMessageSession.Content =  await httpClientSession.SendAsync(requestMessageSession);
                var responseSession = await httpClientSession.SendAsync(requestMessageSession);
                var idUsuarioSession = await responseSession.Content.ReadAsStringAsync();
                
                Console.WriteLine("idUsuarioSession: " + idUsuarioSession);

                if (!responseSession.IsSuccessStatusCode)
                {
                    return Redirect("/Index");
                }
            }
/*
            var httpClient = new HttpClient();
            var url = $"http://localhost:5072/api/Usuario/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            UsuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(content)!;
            */
            return Page();
        }
    }
}