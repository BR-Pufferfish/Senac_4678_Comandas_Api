using Comandas_API.DTOs;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    // Cria a rota do controlador
    [Route("api/[controller]")]
    [ApiController] // Define que essa classe é um controlador de API
    public class CardapioItemController : ControllerBase // Herda de ControllerBase para poder responder a requisições http
    {

        private readonly ComandaDbContext _context;

        public CardapioItemController(ComandaDbContext context)
        {
            _context = context;
        }


        // GET: api/<CardapioItemController>
        [HttpGet] //Anotação que indica se o método responde a requisições GET
        public IResult GetCardapios()
        {
            return Results.Ok(_context.CardapioItem.ToList());
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // Busca o item do cardápio com o id especificado
            var cardapio = _context.CardapioItem.FirstOrDefault(c => c.Id == id);
            
            if (cardapio == null)
                // Se não encontrar, retorna um status 404 (Not Found)
                return Results.NotFound("Cardápio não encontrado...");

            // Retorna o valor para o endpoint da API
            return Results.Ok(cardapio);
        }



        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapioCreate)
        {
            if(cardapioCreate.Titulo.Length < 3)
                return Results.BadRequest("O título deve ter 3 ou mais caracteres...");

            if(cardapioCreate.Descricao.Length < 5)
                return Results.BadRequest("A descrição deve ter 5 ou mais caracteres...");

            if (cardapioCreate.Preco <= 0)
                return Results.BadRequest("O preço deve ser maior que zero...");

            var cardapioItem = new CardapioItem
            {
                Titulo = cardapioCreate.Titulo,
                Descricao = cardapioCreate.Descricao,
                Preco = cardapioCreate.Preco,
                PossuiPreparo = cardapioCreate.PossuiPreparo,
            };

            _context.CardapioItem.Add(cardapioItem);
            _context.SaveChanges();

            return Results.Created($"/api/cardapio/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        // Tres barras /// indica que é um comentário de documentação
        /// <summary>
        ///     Atualiza Cardapio pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardapio"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapioUpdate)
        {
            // Localiza pelo Id
            var cardapioItem = _context.CardapioItem.FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound($"Usuário {id} não encontrado...");

            // Atualiza os dados do cardapio
            cardapioItem.Titulo = cardapioUpdate.Titulo;
            cardapioItem.Descricao = cardapioUpdate.Descricao;
            cardapioItem.Preco = cardapioUpdate.Preco;
            cardapioItem.PossuiPreparo = cardapioUpdate.PossuiPreparo;

            _context.SaveChanges();

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // Localiza pelo Id
            var cardapioItem = _context.CardapioItem.FirstOrDefault(c => c.Id == id);

            // Retorna não encontrado se for null (404)
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado...");

            // Remove o item do cardápio
            _context.CardapioItem.Remove(cardapioItem);
            var removido = _context.SaveChanges();

            // Retorna sem conteudo (204)
            if (removido>0)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
