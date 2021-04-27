using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class UsuariosDomain
    {
        public int idUsuario { get; set; }

        public int idTipoUsuario { get; set; }

        [Required(ErrorMessage = "O endereço de email é obrigatório!")]
        public string email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string senha { get; set; }
    }
}
