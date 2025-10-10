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

        List<CardapioItem> cardapios = new List<CardapioItem>()
        {
            new CardapioItem
            {
                Id = 1,
                Titulo = "Coxinha",
                Descricao = "Apenas um lanche",
                Preco = 5.00M,
                PossuiPreparo = true,
            },
            new CardapioItem
            {
                Id = 2,
                Titulo = "Xis TESTE",
                Descricao = "Delicioso xis de teste",
                Preco = 25.00M,
                PossuiPreparo = true,
            }
        };


        // GET: api/<CardapioItemController>
        [HttpGet] //Anotação que indica se o método responde a requisições GET
        public IResult GetCardapios()
        {
            return Results.Ok(cardapios);
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // Busca o item do cardápio com o id especificado

            // FirstOrDefault retorna o primeiro item que satisfaz a condição ou null se nenhum for encontrado
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            
            if (cardapio == null)
            {
                // Se não encontrar, retorna um status 404 (Not Found)
                return Results.NotFound("Cardápio não encontrado...");
            }

            //retorna o valor para o endpoint da API
            return Results.Ok(cardapio);
        }



        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if(cardapio.Titulo.Length < 3)
            {
                return Results.BadRequest("O título deve ter 3 ou mais caracteres...");
            }
            if(cardapio.Descricao.Length < 5)
            {
                return Results.BadRequest("A descrição deve ter 5 ou mais caracteres...");
            }
            if (cardapio.Preco <= 0)
            {
                return Results.BadRequest("O preço deve ser maior que zero...");
            }
            var cardapioItem = new CardapioItem
            {
                Id = cardapios.Count + 1,
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo,
            };
            cardapios.Add(cardapioItem);
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
        public IResult Put(int id, [FromBody] CardapioItemCreateRequest cardapio)
        {
            // Validações

            // Localiza pelo Id
            var cardapioItem = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound($"Usuário {id} não encontrado...");

            // Atualiza os dados do cardapio
            cardapioItem.Titulo = cardapio.Titulo;
            cardapioItem.Descricao = cardapio.Descricao;
            cardapioItem.Preco = cardapio.Preco;
            cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
