using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class JogosRepository : IJogosRepository
    {
        private string stringConexao = "Data Source=DESKTOP-EEVEMF2\\SQLEXPRESS; initial catalog=inlock_games_manha; user Id=sa; pwd=Leticia0304";
        public void Atualizar(int id, JogosDomain jogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryUpdateIdUrl = "UPDATE jogos SET nomeJogo=@Nome, descricao=@Descricao, dataLancamento=@DataLancamento, valor = @Valor WHERE idJogo = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, con))
                {

                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", jogoAtualizado.nomeJogo);
                    cmd.Parameters.AddWithValue("@Descricao", jogoAtualizado.descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", jogoAtualizado.dataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", jogoAtualizado.valor);


                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogosDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeJogo, descricao, dataLancamento, valor, nomeEstudio FROM jogos INNER JOIN estudios ON jogos.idEstudio = estudios.idEstudio WHERE idJogo = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogosDomain jogoBuscado = new JogosDomain()
                        {
                            nomeJogo = rdr[0].ToString(),
                            descricao = rdr[1].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[2]),
                            valor = Convert.ToInt32(rdr[3]),
                            estudio = new EstudiosDomain()
                            {
                                nomeEstudio = rdr[4].ToString(),
                            }
                        };

                        return jogoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(JogosDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO jogos(nomeJogo, descricao, dataLancamento, valor, idEstudio) VALUES ('" + novoJogo.nomeJogo + "','" + novoJogo.descricao + "', '" + novoJogo.dataLancamento + "', " + novoJogo.valor + ", " + novoJogo.idEstudio + ")";
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM jogos WHERE idJogo = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogosDomain> ListarTodos()
        {
            List<JogosDomain> listaJogos = new List<JogosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT nomeJogo, descricao, dataLancamento, valor, nomeEstudio FROM jogos INNER JOIN estudios ON jogos.idEstudio = estudios.idEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogosDomain jogos = new JogosDomain()
                        {
                            nomeJogo = rdr[0].ToString(),
                            descricao = rdr[1].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[2]),
                            valor = Convert.ToInt32(rdr[3]),
                            estudio = new EstudiosDomain()
                            {
                                nomeEstudio = rdr[4].ToString(),
                            }
                            
                        };

                        listaJogos.Add(jogos);
                    }
                }
            }

            return listaJogos;
        }
    }
}
