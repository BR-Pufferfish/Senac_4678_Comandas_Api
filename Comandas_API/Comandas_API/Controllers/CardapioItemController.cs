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
        // GET: api/<CardapioItemController>
        [HttpGet] //Anotação que indica se o método responde a requisições GET
        public IEnumerable<CardapioItem> Get()
        {
            return new CardapioItem[]
            {
                //Cria uma lista estática de cardapio e transforma em JSON
                new CardapioItem
                {
                    Id = 1,
                    Titulo = "Coxinha",
                    Descricao = "Deliciosa coxinha de frango",
                    Preco = 5.00M,
                    PossuiPreparo = true,
                },
                new CardapioItem
                {
                    Id = 1,
                    Titulo = "Xis Salada",
                    Descricao = "Delicioso xis salada",
                    Preco = 25.00M,
                    PossuiPreparo = true,
                },
            };
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
