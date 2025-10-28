using Comandas_API.DTOs;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {

        private readonly ComandaDbContext _context;
        public ComandaController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(_context.Comanda.ToList());
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comanda.FirstOrDefault(c => c.Id == id);
            if (comanda == null)
                return Results.NotFound("Comanda não encontrada...");

            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter 3 ou mais caracteres...");
            if (comandaCreate.NumeroMesa < 1)
                return Results.BadRequest("O número da mesa deve ser maior que 0...");
            if (comandaCreate.CardapioItemIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos 1 item do cardápio...");

            var novaComanda = new Comanda
            {
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };

            // Cria a lista de itens da comanda
            var comandaItens = new List<ComandaItem>();

            // Cria os itens da comanda
            foreach (var cardapioItemId in comandaCreate.CardapioItemIds)
            {
                var comandaItem = new ComandaItem
                {
                    CardapioItemId = cardapioItemId,
                    ComandaId = novaComanda.Id
                };
                comandaItens.Add(comandaItem);
            }
            novaComanda.Itens = comandaItens;

            _context.Comanda.Add(novaComanda);
            _context.SaveChanges();

            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);
        }

        // PUT api/<ComandaController>/5
        /// <summary>
        ///     Atualiza Comanda pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comandaCreate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaCreateRequest comandaUpdate)
        {
            // Localiza pelo Id
                var comanda = _context.Comanda.FirstOrDefault(c => c.Id == id);

            // Validações
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter 3 ou mais caracteres...");
            if (comandaUpdate.NumeroMesa < 1)
                return Results.BadRequest("O número da mesa deve ser maior que 0...");
            if (comanda is null)
                return Results.NotFound($"Comanda {id} não encontrada...");

            // Atualiza os dados da comanda
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            comanda.NomeCliente = comandaUpdate.NomeCliente;

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // Localiza pelo Id
            var comanda = _context.Comanda.FirstOrDefault(c => c.Id == id);

            // Retorna não encontrado se for null (404)
            if (comanda == null)
                return Results.NotFound($"Comanda {id} não encontrada...");

            // Remove a comanda da lista
            _context.Comanda.Remove(comanda);
            var removido = _context.SaveChanges();

            // Retorna sem conteudo (204)
            if (removido>0)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
