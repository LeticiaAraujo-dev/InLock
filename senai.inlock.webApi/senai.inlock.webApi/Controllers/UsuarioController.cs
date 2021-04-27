using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioRepository.ListarTodos());
        }

        [HttpPost]
        public IActionResult Post(UsuariosDomain novoUsuario)
        {
            if (novoUsuario.email == null)
            {
                return BadRequest("O email é obrigatório!");
            } 
            else if(novoUsuario.senha == null)
            {
                return BadRequest("A senha é obrigatória!");
            }

            _usuarioRepository.Cadastrar(novoUsuario);

            return Created("http://localhost:5000/api/Jogo", novoUsuario);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UsuariosDomain UsuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (UsuarioBuscado != null)
            {
                return Ok(UsuarioBuscado);
            }

            return NotFound("Nenhum usuario encontrado para o identificador informado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UsuariosDomain UsuarioAtualizado)
        {
            UsuariosDomain UsuarioBuscado = _usuarioRepository.BuscarPorId(id);
            if (UsuarioBuscado != null)
            {
                _usuarioRepository.AtualizarIdUrl(id, UsuarioAtualizado);

                return StatusCode(204);
            }
            return NotFound("Nenhum usuario encontrado para o identificador informado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UsuariosDomain UsuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (UsuarioBuscado != null)
            {
                _usuarioRepository.Deletar(id);

                return Ok($"O usuario {id} foi deletado com sucesso!");
            }

            return NotFound("Nenhum usuario encontrado para o identificador informado");
        }
    }
}
