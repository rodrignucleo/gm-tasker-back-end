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
        public IActionResult GetLogin(string? email, string? senha)
        {
            Console.WriteLine(email);
            Console.WriteLine(senha);

            // AuthService _authService = new AuthService();
            var model = _context!.tb_usuario!.FirstOrDefault(x => x.email == email);
            
            if (BCrypt.Net.BCrypt.Verify(senha, model!.senha))
            {
                HttpContext?.Session.SetString("idUsuario", model.id_usuario.ToString());
                Console.WriteLine("Email e Senha igual! id_usuario: "+ HttpContext!.Session.GetString("idUsuario"));
                Console.WriteLine("id da sessao: " + HttpContext.Session.Id);
                return Ok(model);
            }
            else
            {
                Console.WriteLine("Nao esta igual!");
                return NotFound();
            }
        }

        [HttpGet("currentUser")]
        public IActionResult GetSession()
        {
            Console.WriteLine("Id do Usuario na API: " + HttpContext?.Session.GetString("idUsuario"));
            Console.WriteLine("Id da Sessao na API: " + HttpContext?.Session.Id);
            return Ok(HttpContext!.Session.Id);
            /*!.Session.GetString("idUsuario")!;*/
        }
    }
}