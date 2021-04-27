using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class JogosDomain
    {
        public int idJogo { get; set; }

        [Required(ErrorMessage = "O nome do jogo é obrigatório!")]
        public string nomeJogo { get; set; }

        public string descricao { get; set; }

        [DataType(DataType.Date)]
        public DateTime dataLancamento { get; set; }

        [Required(ErrorMessage = "O valor do jogo é obrigatório!")]
        public int valor { get; set; }

        public int idEstudio { get; set; }

        public EstudiosDomain estudio { get; set; }
    }
}
