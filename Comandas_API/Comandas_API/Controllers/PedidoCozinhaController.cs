using Comandas_API.DTOs;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {

        private readonly ComandaDbContext _context;
        public PedidoCozinhaController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/<PedidoController>
        [HttpGet]
        public IResult Get()
        {
            var pedidos = _context.PedidoCozinha
                .Select(p => new PedidoCozinhaResponse
                {
                    Id = p.Id,
                    ComandaId = p.ComandaId,
                    Itens = p.Itens.Select(pi => new PedidoCozinhaItemResponse
                    {
                        Id = pi.Id,
                        Titulo =
                                _context.CardapioItem
                            .First(ci => ci.Id == _context.ComandaItem
                                                    .First(ci => ci.Id == pi.ComandaItemId).CardapioItemId
                             ).Titulo
                    }),
                })
                .ToList();
            return Results.Ok(pedidos);
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var pedido = _context.PedidoCozinha.FirstOrDefault(p => p.Id == id);
            if (pedido == null)
                return Results.NotFound($"Pedido {id} não encontrado...");

            return Results.Ok(pedido);
        }

        // POST api/<PedidoController>
        [HttpPost]
        public IResult Post([FromBody] PedidoCozinhaCreateRequest pedidoCreate)
        {
            if (pedidoCreate.Itens == null || !pedidoCreate.Itens.Any())
                return Results.BadRequest("O pedido deve conter ao menos um item...");
            if (pedidoCreate.ComandaId <= 0)
                return Results.BadRequest("O pedido deve conter uma Comanda válida...");

            var pedido = new PedidoCozinha
            {
                ComandaId = pedidoCreate.ComandaId,
                
            };

            // Cria a lista de itens do pedido
            var itens = new List<PedidoCozinhaItem>();
            foreach (var item in pedidoCreate.Itens)
            {
                var pedidoItem = new PedidoCozinhaItem
                {
                    ComandaItemId = item.ComandaItemId
                };
                itens.Add(pedidoItem);
            }

            pedido.Itens = itens;

            return Results.Created($"/api/pedidoCozinha/{pedido.Id}", pedido);

        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedidoUpdate)
        {
            // Localiza pelo Id
            var pedido = _context.PedidoCozinha.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
                return Results.NotFound($"Pedido {id} não encontrado...");

            // Validações
            if (pedidoUpdate.Itens == null || !pedidoUpdate.Itens.Any())
                return Results.BadRequest("O pedido deve conter ao menos um item...");

            // Atualiza os dados do pedido
            pedido.ComandaId = pedidoUpdate.ComandaId;

            
            _context.SaveChanges();

            // Retorna sem conteudo (204)
            return Results.NoContent();
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // Localiza pelo Id
            var pedido = _context.PedidoCozinha.FirstOrDefault(p => p.Id == id);

            // Retorna não encontrado se for null (404)
            if (pedido == null)
                return Results.NotFound($"Pedido {id} não encontrado...");

            // Remove o pedido da lista
            _context.PedidoCozinha.Remove(pedido);
            var removido = _context.SaveChanges();

            // Retorna sem conteudo (204)
            if (removido>0)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
