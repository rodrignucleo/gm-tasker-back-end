using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Usuario
{
    public class Create : PageModel
    {
        [BindProperty]
        public UsuarioModel UsuarioModel { get; set; } = new();

        
        public Create(){
        }

        public async Task<IActionResult> OnPostAsync(int id){
            if(!ModelState.IsValid){
                return Page();
            }
            
            var httpClient = new HttpClient();
            var url = "http://localhost:5072/api/Usuario";
            var usuarioJson = JsonConvert.SerializeObject(UsuarioModel);
            var content = new StringContent(usuarioJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            
            if(response.IsSuccessStatusCode){
                return RedirectToPage("/Usuario/Index");
            } else {
                return Page();
            }
        }
    }
}