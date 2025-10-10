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
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            // Validações
            if (mesaUpdate.NumeroMesa <= 0)
            {
                return Results.BadRequest("O número da mesa deve ser maior que 0...");
            }
            if (mesaUpdate.Situacao < 0 || mesaUpdate.Situacao > 2)
            {
                return Results.BadRequest("A situação da mesa deve ser 0 (Disponível), 1 (Ocupada) ou 2 (Reservada)...");
            }

            // Localiza pelo Id
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
            {
                return Results.NotFound($"Mesa {id} não encontrada...");
            }

            // Atualiza os dados
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.Situacao = mesaUpdate.Situacao;

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
