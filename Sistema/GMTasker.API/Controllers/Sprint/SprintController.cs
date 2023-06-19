using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Controllers.Sprint
{
    [ApiController]
    [Route("api/[controller]")]
    public class SprintController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public SprintController(AppDbContext context){
            _context = context!;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SprintModel>>> GetSprint(){
            return await _context!.tb_sprint!
                .Include(g => g.Usuario)
                .Include(g => g.Status)
                .OrderBy(g => g.data_conclusao)
                .ToListAsync();
        }

        [HttpGet("{id_sprint}")]
        public async Task<ActionResult<IEnumerable<RequisicaoModel>>> GetSprint(int? id_sprint){
            var sprint = await _context!.tb_sprint!.FindAsync(id_sprint);
            
            if(sprint == null){
                return NotFound();
            }

            return await _context!.tb_requisicao!
                .Include(g => g.UsuarioResponsavel)
                .Include(g => g.Usuario)
                .Include(g => g.Status)
                .Include(g => g.Sprint)
                .OrderBy(g => g.data_prevista_conclusao)
                .Where(u => u.id_atual_responsavel.Equals(id_sprint)).ToListAsync();
        }

        [HttpPut("{id_sprint}")]
        public ActionResult PutSprint(int id_sprint, SprintModel sprint)
        {
            if (id_sprint != sprint.id_sprint)
            {
                return BadRequest();
            }
            
            var model = _context!.tb_sprint!.FirstOrDefault(x => x.id_sprint == id_sprint);
            Console.WriteLine(model!.nome);
            if (model == null)
            {
                return NotFound();
            }

            model.nome = sprint.nome;
            model.descricao = sprint.descricao;
            model.data_cadastro = sprint.data_cadastro;
            model.data_conclusao = sprint.data_conclusao;
            model.id_status = sprint.id_status;
            model.id_usuario_criacao = sprint.id_usuario_criacao;

            _context.tb_sprint!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<SprintModel>> PostSprint(SprintModel sprint){
            _context!.tb_sprint!.Add(sprint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSprint", new{id = sprint.id_sprint}, sprint);
        }

        [HttpDelete("{id_sprint}")]
        public async Task<ActionResult> DeleteSprint(int id_sprint){
            var sprint = await _context!.tb_sprint!.FindAsync(id_sprint);
            if(sprint == null){
                return NotFound();
            }

            _context.tb_sprint.Remove(sprint);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}