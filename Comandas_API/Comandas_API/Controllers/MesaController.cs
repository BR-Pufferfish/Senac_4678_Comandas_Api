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

        static List<Mesa> mesas = new List<Mesa>()
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
        public IResult Post([FromBody] MesaCreateRequest mesaCreate)
        {
            // Validações
            if (mesaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            
            // Cria uma nova mesa
            var novaMesa = new Mesa
            {
                Id = mesas.Count + 1,
                NumeroMesa = mesaCreate.NumeroMesa,
                Situacao = (int)SituacaoMesa.Disponivel
            };

            // Adiciona a nova mesa na lista
            mesas.Add(novaMesa);

            // Retorna a nova mesa criada e o codigo 201 CREATED
            return Results.Created($"/api/mesa/{novaMesa.Id}", novaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            // Validações
            if (mesaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que 0...");

            if (mesaUpdate.Situacao < 0 || mesaUpdate.Situacao > 2)
                return Results.BadRequest("A situação da mesa deve ser 0 (Disponível), 1 (Ocupada) ou 2 (Reservada)...");


            // Localiza pelo Id
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
                return Results.NotFound($"Mesa {id} não encontrada...");

            // Atualiza os dados
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.Situacao = mesaUpdate.Situacao;

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // Localiza pelo Id
            var mesa = mesas.FirstOrDefault(m => m.Id == id);

            // Retorna não encontrado se for null (404)
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada...");

            // Remove a mesa da lista
            var removido = mesas.Remove(mesa);

            // Retorna sem conteudo (204)
            if (removido)
                return Results.NoContent();
            
            return Results.StatusCode(500);
        }
    }
}
