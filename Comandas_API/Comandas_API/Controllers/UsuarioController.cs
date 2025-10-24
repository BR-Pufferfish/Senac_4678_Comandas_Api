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
        // Variável que representa o banco de dados
        private readonly ComandaDbContext _context;
        
        // Construtor
        public UsuarioController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            var usuarios = _context.Usuario.ToList();
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult GetResult(int id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return Results.NotFound("Mesa não encontrada...");

            return Results.Ok(_context.Usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if(usuarioCreate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter 6 ou mais caracteres...");

            if (usuarioCreate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter 6 ou mais caracteres...");

            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
                return Results.BadRequest("O email deve ser válido...");

            var usuario = new Usuario
            {
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha,
            };

            // Adiciona um novo usuario na lista
            _context.Usuario.Add(usuario);
            _context.SaveChanges();

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

            // Localiza pelo Id
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound($"Usuário {id} não encontrado...");

            // Validações
            if (usuarioUpdate.Senha.Length < 6)
                return Results.BadRequest("A senha deve ter 6 ou mais caracteres...");
            
            // Atualiza os dados do usuario
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;

            _context.SaveChanges();

            // Retorna sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // Localiza pelo Id
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);

            // Retorna não encontrado se for null (404)
            if (usuario is null)
                return Results.NotFound($"Usuário {id} não encontrado...");

            // Remove o usuario da lista
             _context.Usuario.Remove(usuario);
            var removido = _context.SaveChanges();

            // Retorna sem conteudo (204)
            if (removido>0)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
