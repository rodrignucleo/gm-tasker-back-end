using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Controllers.Requisicao
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequisicaoController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public RequisicaoController(AppDbContext context){
            _context = context!;
        }

        [HttpGet]
        [Route("{id_usuario}")]
        public async Task<ActionResult<IEnumerable<RequisicaoModel>>> GetRequisicao(int? id_usuario){
            return await _context!.tb_requisicao!
                .Include(g => g.UsuarioResponsavel)
                .Include(g => g.Usuario)
                .Include(g => g.Status)
                .Include(g => g.Sprint)
                .OrderBy(g => g.data_prevista_conclusao)
                .Where(u => u.id_atual_responsavel.Equals(id_usuario)).ToListAsync();
        }

        [HttpGet("req/{id_requisicao}")]
        public async Task<ActionResult<RequisicaoModel>> GetRequisicao(int id_requisicao){
            var requisicao = await _context!.tb_requisicao!.FindAsync(id_requisicao);
            
            if(requisicao == null){
                return NotFound();
            }
            return requisicao;
        }

        [HttpPut("req/{id_requisicao}")]
        public ActionResult PutRequisicao(int id_requisicao, RequisicaoModel requisicao)
        {
            if (id_requisicao != requisicao.id_requisicao)
            {
                return BadRequest();
            }

            var model = _context!.tb_requisicao!.FirstOrDefault(x => x.id_requisicao == id_requisicao);

            if (model == null)
            {
                return NotFound();
            }

            _context.tb_requisicao!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<RequisicaoModel>> PostRequisicao(RequisicaoModel requisicao){
            _context!.tb_requisicao!.Add(requisicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequisicao", new{id = requisicao.id_requisicao}, requisicao);
        }

        [HttpDelete("req/{id_requisicao:int}")]
        public async Task<ActionResult> DeleteRequisicao(int id_requisicao){
            var requisicao = await _context!.tb_requisicao!.FindAsync(id_requisicao);
            if(requisicao == null){
                return NotFound();
            }

            _context.tb_requisicao.Remove(requisicao);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}