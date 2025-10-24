using Comandas_API.DTOs;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        private readonly ComandaDbContext _context;

        public MesaController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/<MesaController>
        [HttpGet]
        public IResult GetMesas()
        {
            return Results.Ok(_context.Mesa);
        }


        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var mesa = _context.Mesa.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
            {
                return Results.NotFound("Mesa não encontrada...");
            }
            return Results.Ok(mesa);
        }


        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody] MesaCreateRequest mesaCreateRequest)
        {
            // Validações
            if (mesaCreateRequest.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            
            // Cria uma nova mesa
            var novaMesa = new Mesa
            {
                NumeroMesa = mesaCreateRequest.NumeroMesa,
                Situacao = (int)SituacaoMesa.Disponivel
            };

            // Adiciona a nova mesa na lista
            _context.Mesa.Add(novaMesa);
            _context.SaveChanges();

            // Retorna a nova mesa criada e o codigo 201 CREATED
            return Results.Created($"/api/mesa/{novaMesa.Id}", novaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdateRequest)
        {
            // Localiza pelo Id
            var mesa = _context.Mesa.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
                return Results.NotFound($"Mesa {id} não encontrada...");

            // Validações
            if (mesaUpdateRequest.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que 0...");
            if (mesaUpdateRequest.Situacao < 0 || mesaUpdateRequest.Situacao > 2)
                return Results.BadRequest("A situação da mesa deve ser 0 (Disponível), 1 (Ocupada) ou 2 (Reservada)...");

            // Atualiza os dados
            mesa.NumeroMesa = mesaUpdateRequest.NumeroMesa;
            mesa.Situacao = mesaUpdateRequest.Situacao;

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // Localiza pelo Id
            var mesa = _context.Mesa.FirstOrDefault(m => m.Id == id);

            // Retorna não encontrado se for null (404)
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada...");

            // Remove a mesa da lista
            _context.Mesa.Remove(mesa);
            var removido = _context.SaveChanges();

            // Retorna sem conteudo (204)
            if (removido>0)
                return Results.NoContent();
            
            return Results.StatusCode(500);
        }
    }
}
