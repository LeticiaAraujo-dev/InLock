using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IEstudiosRepository
    {
        List<EstudiosDomain> ListarTodos();

        EstudiosDomain BuscarPorId(int id);

        void Cadastrar(EstudiosDomain novoEstudio);

        void Atualizar(int id, EstudiosDomain estudioAtualizado);

        void Deletar(int id);
    }
}
