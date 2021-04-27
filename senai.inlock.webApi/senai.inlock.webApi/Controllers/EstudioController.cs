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
    public class EstudioController : ControllerBase
    {
        private IEstudiosRepository _estudiosRepository { get; set; }

        public EstudioController()
        {
            _estudiosRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_estudiosRepository.ListarTodos());
        }

        [HttpPost]
        public IActionResult Post(EstudiosDomain novoEstudio)
        {
            if (novoEstudio.nomeEstudio == null)
            {
                return BadRequest("O nome do estudio é obrigatório!");
            }
            _estudiosRepository.Cadastrar(novoEstudio);

            return Created("http://localhost:44696/api/Estudios", novoEstudio);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudiosDomain EstudioBuscado = _estudiosRepository.BuscarPorId(id);

            if (EstudioBuscado != null)
            {
                return Ok(EstudioBuscado);
            }

            return NotFound("Nenhum estudio encontrado para o identificador informado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, EstudiosDomain EstudioAtualizado)
        {
            EstudiosDomain EstudioBuscado = _estudiosRepository.BuscarPorId(id);
            if (EstudioBuscado != null)
            {
                _estudiosRepository.Atualizar(id, EstudioAtualizado);

                return StatusCode(204);
            }
            return NotFound("Nenhum estudio encontrado para o identificador informado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EstudiosDomain EstudioBuscado = _estudiosRepository.BuscarPorId(id);

            if (EstudioBuscado != null)
            {
                _estudiosRepository.Deletar(id);

                return Ok($"O estudio {id} foi deletado com sucesso!");
            }

            return NotFound("Nenhum estudio encontrado para o identificador informado");
        }


    }
}
