using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using GMTasker.RazorPages.Models;

namespace GMTasker.RazorPages.Pages.Requisicao
{
    public class Index : PageModel
    {
        public List<RequisicaoModel> RequisicaoList { get; set; } = new();

        public Index(){

        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }
            Console.WriteLine("----------------->>>>>>>   AQUI O ID: " + id);
            // GarconList = await _context.Garcon!.ToListAsync();
            var httpClient = new HttpClient();
            var url = $"http://localhost:5072/api/Requisicao/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            RequisicaoList = JsonConvert.DeserializeObject<List<RequisicaoModel>>(content)!;
            
            return Page();
        }
    }
}