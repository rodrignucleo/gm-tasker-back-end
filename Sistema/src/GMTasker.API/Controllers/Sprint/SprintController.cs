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

        [HttpGet("usuSprint/{id_usuario}")]
        public async Task<ActionResult<IEnumerable<SprintModel>>> GetSprint(int id_usuario){
            return await _context!.tb_sprint!
                .Include(g => g.Usuario)
                .Include(g => g.Status)
                .OrderBy(g => g.data_cadastro)
                .Where(x => x.id_usuario_criacao! == id_usuario)
                .ToListAsync();
        }

        [HttpGet]
        [Route("conc/{id_usuario}")]
        public async Task<ActionResult<IEnumerable<SprintModel>>> GetSprintNaoConcluida(int? id_usuario){
            
            var model = _context!.tb_sprint!.FirstOrDefault(x => x.id_usuario_criacao! == id_usuario && x.Status!.nome! != "CONCLUIDO");

            if(model == null){
                return NotFound("Usuário não tem Sprint!");
            }

            return await _context!.tb_sprint!
                .Include(g => g.Status)
                .OrderBy(g => g.data_cadastro)
                .Where(x => x.id_usuario_criacao! == id_usuario)
                .ToListAsync();
        }

        [HttpGet("{id_sprint}")]
        public async Task<ActionResult<IEnumerable<RequisicaoModel>>> GetSprint(int? id_sprint){
            var sprint = await _context!.tb_sprint!.FindAsync(id_sprint);
            
            if(sprint == null){
                return NotFound();
            }

            return Ok(sprint);
        }

        [HttpPut("{id_sprint}/{statusNome}")]
        public ActionResult PutSprint(int id_sprint, SprintModel sprint, String statusNome)
        {
            if (id_sprint != sprint.id_sprint)
            {
                return BadRequest();
            }

            var modelStatus = _context!.tb_status!.FirstOrDefault(x => x.nome == statusNome);
            var model = _context!.tb_sprint!.FirstOrDefault(x => x.id_sprint == id_sprint);
            
            Console.WriteLine(model!.nome);
            if (model == null)
            {
                return NotFound();
            }

            model.nome = sprint.nome;
            model.descricao = sprint.descricao;
            model.data_cadastro = model.data_cadastro;
            model.data_conclusao = sprint.data_conclusao;
            model.id_status = modelStatus!.id_status;
            model.id_usuario_criacao = sprint.id_usuario_criacao;

            _context.tb_sprint!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost("{statusNome}")]
        public async Task<ActionResult<SprintModel>> PostSprint(SprintModel sprint, String statusNome){
            
            var modelStatus = _context!.tb_status!.FirstOrDefault(x => x.nome == statusNome);
            sprint.id_status = modelStatus!.id_status;
            
            
            _context!.tb_sprint!.Add(sprint);
            await _context.SaveChangesAsync();
            CreatedAtAction("GetSprint", new{id = sprint.id_sprint}, sprint);

            return Ok(sprint);
        }

        [HttpDelete("{id_sprint}")]
        public async Task<ActionResult> DeleteSprint(int id_sprint){
            var sprint = await _context!.tb_sprint!.FindAsync(id_sprint);
            if(sprint == null){
                return NotFound();
            }

            _context.tb_sprint.Remove(sprint);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}