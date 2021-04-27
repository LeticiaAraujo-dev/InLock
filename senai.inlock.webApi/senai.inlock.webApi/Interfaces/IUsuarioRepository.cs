using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuariosDomain> ListarTodos();
        UsuariosDomain BuscarPorId(int id);
        UsuariosDomain BuscarPorEmailSenha(string email, string senha);
        void Cadastrar(UsuariosDomain usuarioNovo);
        void AtualizarIdCorpo(UsuariosDomain filme);
        void AtualizarIdUrl(int id, UsuariosDomain filme);
        void Deletar(int id);

    }
}
