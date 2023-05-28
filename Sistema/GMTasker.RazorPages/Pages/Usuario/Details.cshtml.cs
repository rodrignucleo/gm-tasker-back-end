using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Usuario
{
    public class Details : PageModel
    {
        public UsuarioModel UsuarioModel { get; set; } = new();

        public Details(){
            
        }

        public async Task<IActionResult> OnGetAsync(int? id){

            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5072/api/Usuario/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            UsuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(content)!;
            
            return Page();
        }
    }
}