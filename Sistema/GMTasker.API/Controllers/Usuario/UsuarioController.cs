using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Controllers.Usuario
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public UsuarioController(AppDbContext context){
            _context = context!;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuario(){
            return await _context!.tb_usuario!.OrderBy(g => g.nome).ToListAsync();
        }

        [HttpGet("{id_usuario}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuario(int id_usuario){
            var usuario = await _context!.tb_usuario!.FindAsync(id_usuario);
            
            if(usuario == null){
                return NotFound();
            }
            return usuario;
        }

        [HttpPut("{id_usuario}")]
        public async Task<ActionResult> PutUsuario(int id_usuario, UsuarioModel usuario){
            if(id_usuario != usuario.id_usuario){
                return BadRequest();
            }
            usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
            _context!.Entry(usuario).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuario(UsuarioModel usuario){
            usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
            _context!.tb_usuario!.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new{id = usuario.id_usuario}, usuario);
        }

        [HttpDelete("{id_usuario:int}")]
        public async Task<ActionResult> DeleteUsuario(int id_usuario){
            var usuario = await _context!.tb_usuario!.FindAsync(id_usuario);
            if(usuario == null){
                return NotFound();
            }

            _context.tb_usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }


        /*
        [HttpGet]
        [Route("/usuario")]
        public IActionResult Get([FromServices] AppDbContext context){
            var atendimentos = context.Atendimento!.Include(p => p.Mesa).ToList();
            return Ok(atendimentos);
        }
        
        [HttpGet("/usuario/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var atendimentoModel = context.Atendimento!.Include(p => p.Mesa).FirstOrDefault(x => x.AtendimentoId == id);
            if (atendimentoModel == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                AtendimentoId = atendimentoModel.AtendimentoId,
                AtendimentoFechado = atendimentoModel.AtendimentoFechado,
                DataSaida = atendimentoModel.DataSaida,
                MesaId = atendimentoModel.MesaId,
                Mesa = new
                {
                    MesaId = atendimentoModel.Mesa!.MesaId,
                    Numero = atendimentoModel.Mesa.Numero,
                    HoraAbertura = atendimentoModel.Mesa.HoraAbertura
                }
            });
        }

        [HttpPost("/usuario/Create")]
        public IActionResult Post([FromBody] AtendimentoModel atendimentoModel,
            [FromServices] AppDbContext context)
        {
            var atendimentoToAdd = context.Mesa!.FirstOrDefault(x => x.MesaId == atendimentoModel.MesaId);

            if (atendimentoToAdd == null) {
                return NotFound();
            }
            if(atendimentoToAdd!.Status == false){
                context.Atendimento!.Add(atendimentoModel);
                // Altera o status da mesa e sua hora de abertura
                atendimentoToAdd.Status = true;
                atendimentoToAdd.HoraAbertura = DateTime.Now.AddHours(1.50);
                atendimentoModel.DataCriacao = DateTime.Now;
                context.SaveChanges();
                return Created($"/{atendimentoModel.AtendimentoId}", atendimentoModel);
            }
            else{
                return RedirectToPage("/Atendimento/Create");
            }
            
        }

        [HttpPut("/usuario/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id, 
            [FromBody] AtendimentoModel atendimentoModel,
            [FromServices] AppDbContext context)
        {   
            var model = context.Atendimento!.Include(p => p.Mesa).FirstOrDefault(x => x.AtendimentoId == id);
            if (model == null) {
                return NotFound();
            }
            var mesaAntigaId = model.MesaId;
            var mesaAntiga = context.Mesa!.FirstOrDefault(x => x.MesaId == atendimentoModel.MesaId);
            mesaAntiga!.Status = true;
            mesaAntiga!.HoraAbertura = DateTime.Now.AddHours(1);
            
            model.MesaId = atendimentoModel.MesaId;
            model.Mesa!.Status = false;
            model.Mesa!.HoraAbertura = null;

            context.Atendimento!.Update(model);

            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/usuario/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id, 
            [FromServices] AppDbContext context)
        {
            var atendimentoToDelete = context.Atendimento!.Include(p => p.Mesa).FirstOrDefault(x => x.AtendimentoId == id);

            if (atendimentoToDelete == null) {
                return NotFound();
            }

            // Altera o status da mesa e sua hora de abertura
            atendimentoToDelete.Mesa!.Status = false;
            atendimentoToDelete.Mesa.HoraAbertura = null;
            
            context.Atendimento!.Remove(atendimentoToDelete);
            context.SaveChanges();
            
            return Ok(atendimentoToDelete);
        }*/
    }
}