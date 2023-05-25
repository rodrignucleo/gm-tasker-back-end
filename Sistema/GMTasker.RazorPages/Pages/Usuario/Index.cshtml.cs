using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Usuario
{
    public class Index : PageModel
    {
        public List<UsuarioModel> UsuarioList { get; set; } = new();

        public Index(){

        }

        public async Task<IActionResult> OnGetAsync(){
            // GarconList = await _context.Garcon!.ToListAsync();
            var httpClient = new HttpClient();
            var url = "http://localhost:32770/api/Usuario";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            UsuarioList = JsonConvert.DeserializeObject<List<UsuarioModel>>(content)!;
            
            return Page();
        }
    }
}