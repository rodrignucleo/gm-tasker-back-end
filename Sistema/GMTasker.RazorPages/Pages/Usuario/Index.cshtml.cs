using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Usuario
{
    public class Index : PageModel
    {
        // private readonly AppDbContext _context;

        public List<UsuarioModel> UsuarioList { get; set; } = new();

        // public Index(AppDbContext context){
        //     _context = context;
        // }

        public Index(){

        }

        public async Task<IActionResult> OnGetAsync(){
            // GarconList = await _context.Garcon!.ToListAsync();
            var httpClient = new HttpClient();
            var url = "http://localhost:5171/Garcon";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            GarconList = JsonConvert.DeserializeObject<List<GarconModel>>(content)!;
            
            return Page();
        }
    }
}