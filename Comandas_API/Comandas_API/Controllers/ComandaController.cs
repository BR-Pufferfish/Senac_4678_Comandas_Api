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

        List<Comanda> comandas = new List<Comanda>()
        {
            new Comanda
            {
                Id = 1,
                NumeroMesa = 1,
                NomeCliente = "Aninha do grau",
            },
            new Comanda
            {
                Id = 2,
                NumeroMesa = 1,
                NomeCliente = "Aninha do grau",
            },
            new Comanda
            {
                Id = 1,
                NumeroMesa = 3,
                NomeCliente = "Aninha do grau",
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
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
            {
                return Results.BadRequest("O nome do cliente deve ter 3 ou mais caracteres...");
            }
            if (comandaCreate.NumeroMesa < 1)
            {
                return Results.BadRequest("O número da mesa deve ser maior que 0...");
            }
            if (comandaCreate.CardapioItemIds.Length == 0)
            {   
                return Results.BadRequest("A comanda deve ter pelo menos 1 item do cardápio...");
            }

            var novaComanda = new Comanda
            {
                Id = comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };
            comandas.Add(novaComanda);
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaCreateRequest comandaUpdate)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda == null)
                return Results.NotFound($"Comanda {id} não encontrada...");

            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            comanda.NomeCliente = comandaUpdate.NomeCliente;

        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
