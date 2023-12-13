using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            Console.WriteLine("id do usuario da requisicao: " + id_usuario);

            var model = _context!.tb_requisicao!.Where(u => u.Status!.nome! != "CONCLUIDO").FirstOrDefault(x => x.id_atual_responsavel == id_usuario);

            if (model == null)
            {
                return NotFound();
            }

            return await _context!.tb_requisicao!
                .Include(g => g.UsuarioResponsavel)
                .Include(g => g.Usuario)
                .Include(g => g.Status)
                .Include(g => g.Sprint)
                .OrderBy(g => g.data_cadastro)
                .Where(u => u.id_atual_responsavel.Equals(id_usuario))
                .Where(u => u.Status!.nome! != "CONCLUIDO")
                .ToListAsync();
        }

        [HttpGet]
        [Route("conc/{id_usuario}")]
        public async Task<ActionResult<IEnumerable<RequisicaoModel>>> GetRequisicaoConcluida(int? id_usuario){
            Console.WriteLine("id do usuario da requisicao: " + id_usuario);

            var model = _context!.tb_requisicao!
            .Where(u => u.Status!.nome!.Equals("CONCLUIDO"))
            .FirstOrDefault(x => x.id_atual_responsavel == id_usuario);
            Console.WriteLine(model!.nome);
            
            if (model == null)
            {
                return NotFound();
            }

            return await _context!.tb_requisicao!
                .Include(g => g.UsuarioResponsavel)
                .Include(g => g.Usuario)
                .Include(g => g.Status)
                .Include(g => g.Sprint)
                .OrderBy(g => g.data_cadastro)
                .Where(u => u.id_atual_responsavel.Equals(id_usuario))
                .Where(u => u.Status!.nome!.Equals("CONCLUIDO"))
                .ToListAsync();
        }

        [HttpGet("req/{id_requisicao}")]
        public async Task<ActionResult<RequisicaoModel>> GetRequisicao(int id_requisicao){
            var requisicao = await _context!.tb_requisicao!.FindAsync(id_requisicao);
            
            if(requisicao == null){
                return NotFound();
            }
            return requisicao;
        }

        [HttpGet("sprint/{id_sprint}")]
        public ActionResult<IEnumerable<RequisicaoModel>> GetRequisicaoSprint(int id_sprint){

            var requisicao = _context!.tb_requisicao!
            .Include(g => g.UsuarioResponsavel)
            .Include(g => g.Usuario)
            .Include(g => g.Status)
            .Include(g => g.Sprint)
            .Where(u => u.id_sprint == id_sprint)
            .OrderBy(g => g.data_cadastro);

            if (requisicao == null)
            {
                return NotFound();
            }

            return Ok(requisicao);
        }


        [HttpPut("concluir/{id_requisicao}")]
        public ActionResult PutRequisicaoStatus(int id_requisicao)
        {
            var model = _context!.tb_requisicao!.FirstOrDefault(x => x.id_requisicao == id_requisicao);

            if (model == null)
            {
                return NotFound();
            }

            model.id_status = 3;

            _context.tb_requisicao!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPut("req/{id_requisicao}/{email}/{statusNome}")]
        public ActionResult PutRequisicao(int id_requisicao, RequisicaoModel requisicao, String? email, String statusNome)
        {
            if (id_requisicao != requisicao.id_requisicao)
            {
                return BadRequest();
            }

            Console.WriteLine(" ------------> ID DA SPRINT AQUI: " + requisicao.id_sprint);
            Console.WriteLine(" ------------> ID DA STATUS AQUI: " + requisicao.id_status);
            var modelUsuario = _context!.tb_usuario!.FirstOrDefault(x => x.email == email);
            
            if (email == "")
            {
                return NotFound("Email não digitado");
            }
            if (modelUsuario == null)
            {
                return NotFound("Email não encontrado");
            }

            var modelStatus = _context!.tb_status!.FirstOrDefault(x => x.nome == statusNome);

            if(requisicao.id_sprint == 0){
                requisicao.id_sprint = null;
            }
            
            var model = _context!.tb_requisicao!.FirstOrDefault(x => x.id_requisicao == id_requisicao);

            if (model == null)
            {
                return NotFound();
            }

            model.nome = requisicao.nome;
            model.descricao = requisicao.descricao;
            model.data_cadastro = requisicao.data_cadastro;
            model.data_conclusao = requisicao.data_conclusao;
            model.id_status = modelStatus!.id_status;
            model.id_atual_responsavel = modelUsuario!.id_usuario;
            model.id_usuario_criacao = requisicao.id_usuario_criacao;
            model.id_sprint = requisicao.id_sprint;

            _context.tb_requisicao!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost("{email}/{statusNome}")]
        public async Task<ActionResult<RequisicaoModel>> PostRequisicao(RequisicaoModel requisicao, String? email, String statusNome){
            
            // requisicao.data_cadastro = DateTime.Today;
            Console.WriteLine(" ------------> ID DA SPRINT AQUI: " + requisicao.id_sprint);
            Console.WriteLine(" ------------> ID DA STATUS AQUI: " + requisicao.id_status);
            var model = _context!.tb_usuario!.FirstOrDefault(x => x.email == email);
            
            if (email == "")
            {
                return NotFound("Email não digitado");
            }
            if (model == null)
            {
                return NotFound("Email não encontrado");
            }

            requisicao.id_atual_responsavel = model!.id_usuario;

            var modelStatus = _context!.tb_status!.FirstOrDefault(x => x.nome == statusNome);
            requisicao.id_status = modelStatus!.id_status;

            if(requisicao.id_sprint == 0){
                requisicao.id_sprint = null;
            }

            // requisicao.data_cadastro = DateTime.Now;

            _context!.tb_requisicao!.Add(requisicao);
            await _context.SaveChangesAsync();
            CreatedAtAction("GetRequisicao", new{id = requisicao.id_requisicao}, requisicao);

            return Ok(requisicao);
        }

        [HttpDelete("req/{id_requisicao}")]
        public async Task<ActionResult> DeleteRequisicao(int id_requisicao){
            var requisicao = await _context!.tb_requisicao!.FindAsync(id_requisicao);
            if(requisicao == null){
                return NotFound();
            }

            _context.tb_requisicao.Remove(requisicao);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}