using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {

        List<PedidoCozinha> pedidosCozinha = new List<PedidoCozinha>()
        {
            new PedidoCozinha
            {
                Id = 1,
                ComandaId = 1,
            },
            new PedidoCozinha
            {
                Id = 2,
                ComandaId = 1,
            },
        };

        // GET: api/<PedidoController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(pedidosCozinha);
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var pedido = pedidosCozinha.FirstOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                return Results.NotFound("Mesa não encontrada...");
            }
            return Results.Ok(var pedido = pedidosCozinha.FirstOrDefault(p => p.Id == id);
);
        }

        // POST api/<PedidoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
