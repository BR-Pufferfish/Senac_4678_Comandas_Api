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
            var comandas = _context.Comanda
                .Select( c => new ComandaCreateResponse
                {
                    Id = c.Id,
                    NomeCliente = c.NomeCliente,
                    NumeroMesa = c.NumeroMesa,
                    Itens = c.Itens.Select(i => new ComandaItemResponse
                    {
                        Id = i.Id,
                        Titulo = _context.CardapioItem.First(ci => ci.Id == i.CardapioItemId).Titulo
                    }).ToList()
                }).ToList();
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comanda
                .Select(c => new ComandaCreateResponse
                {
                    Id = c.Id,
                    NomeCliente = c.NomeCliente,
                    NumeroMesa = c.NumeroMesa,
                    Itens = c.Itens.Select(i => new ComandaItemResponse
                    {
                        Id = i.Id,
                        Titulo = _context.CardapioItem.First(ci => ci.Id == i.CardapioItemId).Titulo
                    }).ToList()
                })
                .FirstOrDefault(c => c.Id == id);
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
                    Comanda = novaComanda
                };

                // Adiciona o item na lista de itens da comanda
                comandaItens.Add(comandaItem);

                // Criar o pedido de cozinha e o item de acordo com o cardapio possuiPreparo
                var cardapioItem = _context.CardapioItem.FirstOrDefault(ci => ci.Id == comandaItem.CardapioItemId);
                if (cardapioItem!.PossuiPreparo)
                {
                    var pedido = new PedidoCozinha
                    {
                        Comanda = novaComanda,

                    };
                    _context.PedidoCozinha.Add(pedido);
                    var pedidoItem = new PedidoCozinhaItem
                    {
                        PedidoCozinha = pedido,
                        ComandaItem = comandaItem,
                    };
                    _context.PedidoCozinha.Add(pedido);
                    _context.PedidoCozinhaItem.Add(pedidoItem);
                }
            }

            // Atribui os itens à comanda
            novaComanda.Itens = comandaItens;
            _context.Comanda.Add(novaComanda);
            _context.SaveChanges();

            var response = new ComandaCreateResponse
            {
                Id = novaComanda.Id,
                NomeCliente = novaComanda.NomeCliente,
                NumeroMesa = novaComanda.NumeroMesa,
                Itens = novaComanda.Itens.Select(i => new ComandaItemResponse
                {
                    Id = i.Id,
                    Titulo = _context.CardapioItem.First(ci => ci.Id == i.CardapioItemId).Titulo
                }).ToList()
            };

            return Results.Created($"/api/comanda/{response.Id}", response);
        }

        // PUT api/<ComandaController>/5
        /// <summary>
        ///     Atualiza Comanda pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comandaCreate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
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

            // Percorre os itens para adicionar ou remover
            foreach (var itemUpdate in comandaUpdate.Itens)
            {
                if(itemUpdate.Id >0 && itemUpdate.Remove == true)
                {
                    //removendo
                    RemoverItemComanda(itemUpdate.Id);
                }
                if(itemUpdate.CardapioItemId >0)
                {
                    //adicionando
                    InserirItemComanda(comanda, itemUpdate.CardapioItemId);
                }
            }

            _context.SaveChanges();
            // Retorna sem conteudo
            return Results.NoContent();
        }

        private void InserirItemComanda(Comanda comanda, int cardapioItemId)
        {
            _context.ComandaItem.Add(new ComandaItem
            {
                CardapioItemId = cardapioItemId,
                Comanda = comanda
            });
        }

        private void RemoverItemComanda(int id)
        {
            var comandaItem = _context.ComandaItem.FirstOrDefault(ci => ci.Id == id);
            if (comandaItem != null)
            {
                _context.ComandaItem.Remove(comandaItem);
            }
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
