using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GMTasker.API.Controllers.Status
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public StatusController(AppDbContext context){
            _context = context!;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusModel>>> GetStatusRequisicao(){
            return await _context!.tb_status!.ToListAsync();
        }

        [HttpGet("{id_status}")]
        public async Task<ActionResult<StatusModel>> GetStatusRequisicao(int id_status){
            var status = await _context!.tb_status!.FindAsync(id_status);
            
            if(status == null){
                return NotFound();
            }
            return status;
        }

        [HttpPut("req/{id_requisicao}")]
        public ActionResult PutRequisicao(int id_requisicao, RequisicaoModel requisicao)
        {
            if (id_requisicao != requisicao.id_requisicao)
            {
                return BadRequest();
            }
            
            var model = _context!.tb_requisicao!.FirstOrDefault(x => x.id_requisicao == id_requisicao);
            Console.WriteLine(model!.nome);
            if (model == null)
            {
                return NotFound();
            }

            model.nome = requisicao.nome;
            model.descricao = requisicao.descricao;
            model.data_cadastro = requisicao.data_cadastro;
            model.data_conclusao = requisicao.data_conclusao;
            model.id_status = requisicao.id_status;
            model.id_atual_responsavel = requisicao.id_atual_responsavel;
            model.id_usuario_criacao = requisicao.id_usuario_criacao;
            model.id_sprint = requisicao.id_sprint;

            _context.tb_requisicao!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<RequisicaoModel>> PostRequisicao(RequisicaoModel requisicao){
            
            // requisicao.data_cadastro = DateTime.Today;
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