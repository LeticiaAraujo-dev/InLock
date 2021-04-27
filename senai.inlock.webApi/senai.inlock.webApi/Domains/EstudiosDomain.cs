using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class EstudiosDomain
    {
        public int idEstudio { get; set; }

        [Required(ErrorMessage = "O nome do estudio é obrigatório!")]
        public string nomeEstudio { get; set; }
    }
}
