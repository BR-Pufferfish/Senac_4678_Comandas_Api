using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@admin.com",
                Senha = "admin",
            },
            new Usuario
            {
                Id = 2,
                Nome = "Usuario",
                Email = "usuario@usuario.com",
                Senha = "usuario",
            },
        };


        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return Results.NotFound("Mesa não encontrada...");
            }
            return Results.Ok(usuarios);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResul Post([FromBody] Usuario usuario)
        {
            //adiciona um novo usuario na lista
            usuarios.Add(usuario);
            
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
