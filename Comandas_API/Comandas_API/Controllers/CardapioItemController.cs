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
        public void Post([FromBody] CardapioItemCreateRequest cardapio)
        {

        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CardapioItemCreateRequest cardapio)
        {

        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
