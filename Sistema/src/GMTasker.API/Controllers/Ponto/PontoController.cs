using GMTasker.API.Models;
using GMTasker.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Org.BouncyCastle.Asn1.Cms;

namespace GMTasker.API.Controllers.Ponto
{
    [ApiController]
    [Route("api/[controller]")]
    public class PontoController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public PontoController(AppDbContext context){
            _context = context!;
        }

        [HttpGet("usuPonto/{id_usuario}")]
        public async Task<ActionResult<IEnumerable<PontoModel>>> GetPonto(int id_usuario){
            return await _context!.tb_ponto!
                .Include(g => g.Usuario)
                .OrderBy(g => g.data_ponto)
                .Where(x => x.id_usuario_criacao! == id_usuario)
                .ToListAsync();
        }

        [HttpGet("{id_usuario}")]
        public async Task<ActionResult<IEnumerable<RequisicaoModel>>> GetPonto(int? id_usuario, String data_ponto){

            var Ponto = _context!.tb_ponto!
            .Where(x => x.id_usuario_criacao! == id_usuario)
            .Where(g => g.data_ponto == data_ponto)
            .OrderBy(g => g.data_ponto);
            
            if(Ponto == null){
                return NotFound();
            }

            return Ok(Ponto);
        }

        [HttpPut("{id_Ponto}")]
        public ActionResult PutPonto(int id_Ponto, PontoModel Ponto)
        {
            if (id_Ponto != Ponto.id_ponto)
            {
                return BadRequest();
            }
            
            var model = _context!.tb_ponto!.FirstOrDefault(x => x.id_ponto == id_Ponto);
            
            if (model == null)
            {
                return NotFound();
            }

            model.data_ponto = Ponto.data_ponto;
            model.hora_ponto = Ponto.hora_ponto;

            _context.tb_ponto!.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost("{id_usuario}")]
        public async Task<ActionResult<PontoModel>> PostPonto(int id_usuario, PontoModel Ponto){

            var PontoSaida = _context!.tb_ponto!
            .Where(x => x.id_usuario_criacao! == Ponto.id_usuario_criacao)
            .Where(g => g.data_ponto == Ponto.data_ponto);


            _context!.tb_ponto!.Add(Ponto);
            await _context.SaveChangesAsync();
            CreatedAtAction("GetPonto", new{id = Ponto.id_ponto}, Ponto);

            return Ok(Ponto);
        }

        [HttpDelete("{id_Ponto}")]
        public async Task<ActionResult> DeletePonto(int id_Ponto){
            var Ponto = await _context!.tb_ponto!.FindAsync(id_Ponto);
            if(Ponto == null){
                return NotFound();
            }

            _context.tb_ponto.Remove(Ponto);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}