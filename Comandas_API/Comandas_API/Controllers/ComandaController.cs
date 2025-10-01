using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {

        List<Comanda> comandas = new List<Comanda>()
        {
            new Comanda
            {
                Id = 1,
                NumeroMesa = 1,
                NomeCliente = "Aninha do grau",
                EstaFechada = false,
            },
            new Comanda
            {
                Id = 2,
                NumeroMesa = 1,
                NomeCliente = "Aninha do grau",
                EstaFechada = true,
            },
            new Comanda
            {
                Id = 1,
                NumeroMesa = 3,
                NomeCliente = "Aninha do grau",
                EstaFechada = false,
            },
        };

        // GET: api/<ComandaController>
        [HttpGet]
        public IResult GetComandas()
        {
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda == null)
            {
                return Results.NotFound("Comanda não encontrada...");
            }
            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
