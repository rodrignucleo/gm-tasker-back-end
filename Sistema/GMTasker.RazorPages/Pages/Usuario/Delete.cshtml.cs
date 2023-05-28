using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Usuario
{
    public class Delete : PageModel {   
        [BindProperty]
        public UsuarioModel UsuarioModel { get; set; } = new();
        public Delete(){
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

        public async Task<IActionResult> OnPostAsync(int id){
            var httpClient = new HttpClient();
            var url = $"http://localhost:5072/api/Usuario/Delete/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await httpClient.SendAsync(requestMessage);

            Console.Out.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Usuario/Index");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                // return NotFound();
                return RedirectToPage("/Usuario/Index");
            }
        }
    }
}