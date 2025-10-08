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

        List<Mesa> mesas = new List<Mesa>()
        {
            new  Mesa
            {
                Id = 1,
                NumeroMesa = 1,
                Situacao = (int)SituacaoMesa.Disponivel,
            },
            new  Mesa
            {
                Id = 2,
                NumeroMesa = 2,
                Situacao = (int)SituacaoMesa.Ocupada,
            },
        };

        // GET: api/<MesaController>
        [HttpGet]
        public IResult GetMesas()
        {
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
            {
                return Results.NotFound("Mesa não encontrada...");
            }
            return Results.Ok(mesa);
        }



        // POST api/<MesaController>
        [HttpPost]
        public void Post([FromBody] MesaCreateRequest mesaCreate)
        {
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
