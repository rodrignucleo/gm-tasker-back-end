using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public ActionResult PutUsuario(UsuarioModel usuario)
        {
            var model = _context!.tb_usuario!.FirstOrDefault(x => x.email == usuario.email);

            if (model == null)
            {
                return NotFound();
            }
            // usuario.senha_antiga = BCrypt.Net.BCrypt.HashPassword(usuario.senha_antiga);
            Console.WriteLine("Email Digitado: " + usuario.senha);
            Console.WriteLine("Senha Digitada: " + usuario.senha);

            Console.WriteLine("Email Atual: " + usuario.senha_antiga);
            Console.WriteLine("Senha Atual: " + model!.senha_antiga);

            if (BCrypt.Net.BCrypt.Verify(usuario.senha, model!.senha))
            {
                return NotFound();
            }
            else
            {
                Console.WriteLine("Nao esta igual!");
                return NotFound();
            }
        }


        // [HttpPost]
        // public void ChecarLogin(){
        //     var usuario = new UsuarioModel();
        // usuario. = Request["login"];
        // usuario.senha = Request["senha"];
            
        // }
        // public async Task<ActionResult<UsuarioModel>> PostLogin(UsuarioModel usuario){
        //     usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
        //     _context!.tb_usuario!.Add(usuario);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetUsuario", new{id = usuario.id_usuario}, usuario);
        // }
    }
}