using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace GMTasker.API.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public LoginController(AppDbContext context){
            _context = context!;
        }

        [HttpPost()]
        [Route("{email}/{senha}")]
        public IActionResult GetUsuarios(string email, string? senha)
        {
            Console.WriteLine(email);
            Console.WriteLine(senha);
            var model = _context!.tb_usuario!.FirstOrDefault(x => x.email == email);
            
            if (BCrypt.Net.BCrypt.Verify(senha, model!.senha))
            {
                Console.WriteLine("Email e Senha igual!");
                return Ok(model);
            }
            else
            {
                Console.WriteLine("Nao esta igual!");
                return NotFound();
            }
        }
    }
}