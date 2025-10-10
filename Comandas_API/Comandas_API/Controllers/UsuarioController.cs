using Comandas_API.DTOs;
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
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if(usuarioCreate.Senha.Length < 6)
            {
                return Results.BadRequest("A senha deve ter 6 ou mais caracteres...");
            }
            if (usuarioCreate.Nome.Length < 3)
            {
                return Results.BadRequest("O nome deve ter 6 ou mais caracteres...");
            }
            if (usuarioCreate.Email.Length < 5 || usuarioCreate.Email.Contains("@"))
            {
                return Results.BadRequest("O email deve ser válido...");
            }

            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha,
            };

            //adiciona um novo usuario na lista
            usuarios.Add(usuario);
            
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }

        // PUT api/<UsuarioController>/5
        /// <summary>
        /// Atualiza um usuario
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <param name="usuarioUpdate">Dados do usuario</param>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            // Validações
            if (usuarioUpdate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter 6 ou mais caracteres...");

            // Localiza pelo Id
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound($"Usuário {id} não encontrado...");
            
            // Atualiza os dados do usuario
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
