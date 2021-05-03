using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        [Authorize(Roles = "1")]
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

        [Authorize]
        [HttpPost("Login")]
        public IActionResult Login(UsuariosDomain login)
        {
            UsuariosDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.email, login.senha);

            if (usuarioBuscado == null)
            {
                return NotFound("E-mail ou senha inválidos!");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.idTipoUsuario.ToString()),
            };

            // Define a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("games-chave-autenticacao"));

            // Define as credenciais do token - Header
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gerar o token
            var token = new JwtSecurityToken(
                issuer: "InLock.webApi",                
                audience: "InLock.webApi",             
                claims: claims,                         
                expires: DateTime.Now.AddHours(2),   
                signingCredentials: creds               
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }


        [Authorize]
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

        [Authorize(Roles = "1")]
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

        [Authorize(Roles = "1")]
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
