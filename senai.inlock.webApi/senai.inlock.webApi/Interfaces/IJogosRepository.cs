using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IJogosRepository
    {
        List<JogosDomain> ListarTodos();
        JogosDomain BuscarPorId(int id);
        void Cadastrar(JogosDomain novoJogo);
        void Atualizar(int id, JogosDomain jogoAtualizado);
        void Deletar(int id);
    }
}
