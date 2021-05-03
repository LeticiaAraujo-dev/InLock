using Microsoft.AspNetCore.Authorization;
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
    public class JogoController : ControllerBase
    {
        private IJogosRepository _jogosRepository { get; set; }

        public JogoController()
        {
            _jogosRepository = new JogosRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_jogosRepository.ListarTodos());
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(JogosDomain novoJogo)
        {
            if (novoJogo.nomeJogo == null)
            {
                return BadRequest("O nome do jogo é obrigatório!");
            } 
            _jogosRepository.Cadastrar(novoJogo);

            return Created("http://localhost:5000/api/Jogo", novoJogo);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            JogosDomain JogoBuscado = _jogosRepository.BuscarPorId(id);

            if (JogoBuscado != null)
            {
                return Ok(JogoBuscado);
            }

            return NotFound("Nenhum jogo encontrado para o identificador informado");
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, JogosDomain JogoAtualizado)
        {
            JogosDomain JogoBuscado = _jogosRepository.BuscarPorId(id);
            if (JogoBuscado != null)
            {
                _jogosRepository.Atualizar(id, JogoAtualizado);

                return StatusCode(204);
            }
            return NotFound("Nenhum jogo encontrado para o identificador informado");
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            JogosDomain JogoBuscado = _jogosRepository.BuscarPorId(id);

            if (JogoBuscado != null)
            {
                _jogosRepository.Deletar(id);

                return Ok($"O jogo {id} foi deletado com sucesso!");
            }

            return NotFound("Nenhum jogo encontrado para o identificador informado");
        }


    }
}
