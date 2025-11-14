using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas_API;
using Comandas_API.Models;

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ComandaDbContext _context;

        public ReservasController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReserva()
        {
            return await _context.Reserva.ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }

            // Atualiza
            _context.Entry(reserva).State = EntityState.Modified;

            // Remoção e inclusão da mesa na reserva (2 reservada / 1 livre)
            // Inverter as situações das mesas para  (2 livre / 1 reservada)


            var novaMesa = await _context.Mesa.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (novaMesa is null)
                return BadRequest("Mesa não encontrada...");
            novaMesa.SituacaoMesa = (int)SituacaoMesa.Reservada;

            // Consulta os dados da reserva origina
            var reservaOriginal = await _context.Reserva.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            // Consulta numero da mesa original
            var numeroMesaOriginal = reservaOriginal!.NumeroMesa;
            // consulta a mesa original
            var mesaOriginal = await _context.Mesa.FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesaOriginal);
            mesaOriginal!.SituacaoMesa = (int)SituacaoMesa.Livre;

            try
            {
                // Salva
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            _context.Reserva.Add(reserva);

            // Atualiza o status da mesa para "Reservada" ao criar uma reserva
            var mesa = await _context.Mesa.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            
            if(mesa is null)
            {
                return BadRequest("Mesa não encontrada...");
            }
            
            if (mesa is not null)
            {
                if(mesa.SituacaoMesa != (int)SituacaoMesa.Livre)
                {
                    return BadRequest("Mesa não disponível para reserva...");
                }

                mesa.SituacaoMesa = (int)SituacaoMesa.Reservada;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            // Atualiza o status da mesa para "Livre" ao cancelar uma reserva
            var mesa = await _context.Mesa.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            
            if (mesa is null) 
            {
                return BadRequest("Mesa não encontrada...");
            }

            // Define a situação da mesa como Livre ao excluir a reserva
            mesa.SituacaoMesa = (int)SituacaoMesa.Livre;



            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }
    }
}
