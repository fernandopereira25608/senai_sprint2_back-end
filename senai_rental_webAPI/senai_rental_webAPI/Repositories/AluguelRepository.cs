using senai_rental_webAPI.Domains;
using senai_rental_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private string stringConexao = @"Data source=DESKTOP-PJVB3DK\SQLEXPRESS; initial catalog=rental; integrated security=true";
        public void Atualizar(AluguelDomain novoAluguel)
        {
            if (novoAluguel.IdAluguel != 0 && novoAluguel.IdVeiculo != 0 && novoAluguel.IdCliente != 0 && novoAluguel.valorAluguel != 0)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdate = "UPDATE aluguel SET IdVeiculo = @IdVeiculo, IdCliente = @IdCliente, valorAluguel = @valorAluguel" +
                        "cpfCliente = @cpfCliente WHERE IdAluguel = @IdAluguel";

                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                    {
                        cmd.Parameters.AddWithValue("@IdVeiculo", novoAluguel.IdVeiculo);
                        cmd.Parameters.AddWithValue("@IdCliente", novoAluguel.IdCliente);
                        cmd.Parameters.AddWithValue("@IdAluguel", novoAluguel.IdAluguel);
                        cmd.Parameters.AddWithValue("@valorAluguel", novoAluguel.valorAluguel);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public AluguelDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelectById = "SELECT IdAluguel, IdCliente, IdVeiculo, valorAluguel, dataInicio [Início], " +
                    "dataFim Fim, nomeCliente Nome, sobrenomeCliente Sobrenome, placaVeiculo [Veículo], nomeModelo Modelo FROM aluguel" +
                    "INNER JOIN veiculo ON veiculo.IdVeiculo = aluguel.IdVeiculo" +
                    "INNER JOIN modelo ON veiculo.IdModelo = modelo.IdModelo" +
                    "WHERE IdAluguel = @IdAluguel";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@IdAluguel", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        ClienteDomain clienteAluguel = new ClienteDomain()
                        {
                            nomeCliente = rdr[6].ToString(),
                            sobrenomeCliente = rdr[7].ToString(),
                        };

                        ModeloDomain modeloCarro = new ModeloDomain();
                        modeloCarro.nomeModelo = rdr[9].ToString();

                        VeiculoDomain veiculoAluguel = new VeiculoDomain()
                        {
                            placaVeiculo = rdr[8].ToString(),
                            modelo = modeloCarro
                        };

                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            IdAluguel = Convert.ToInt32(rdr[0]),
                            IdCliente = Convert.ToInt32(rdr[1]),
                            IdVeiculo = Convert.ToInt32(rdr[2]),
                            valorAluguel = Convert.ToDecimal(rdr[3]),
                            dataInicio = Convert.ToDateTime(rdr[4]),
                            dataFim = Convert.ToDateTime(rdr[5]),
                            cliente = clienteAluguel,
                            veiculo = veiculoAluguel
                        };
                        return aluguel;
                    }
                    return null;
                }
            }
        }

        public void Deletar(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM aluguel WHERE IdAluguel = @IdAluguel";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@IdAluguel", idAluguel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inserir(AluguelDomain aluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO aluguel (IdCliente, IdVeiculo, valorAluguel, dataInicio, dataFim) VALUES (@IdCliente, @IdVeiculo, @valorAluguel, @dataInicio, @dataFim);";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@IdCliente", aluguel.IdCliente);
                    cmd.Parameters.AddWithValue("@IdVeiculo", aluguel.IdVeiculo);
                    cmd.Parameters.AddWithValue("@valorAluguel", aluguel.valorAluguel);
                    cmd.Parameters.AddWithValue("@dataInicio", aluguel.dataInicio);
                    cmd.Parameters.AddWithValue("@dataFim", aluguel.dataFim);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AluguelDomain> ListarTodos()
        {
            List<AluguelDomain> listaAlugueis = new List<AluguelDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdAluguel, IdCliente, IdVeiculo, valorAluguel, dataInicio [Início], " +
                    "dataFim Fim, nomeCliente Nome, sobrenomeCliente Sobrenome, placaVeiculo [Veículo], nomeModelo Modelo FROM aluguel" +
                    "INNER JOIN veiculo ON veiculo.IdVeiculo = aluguel.IdVeiculo" +
                    "INNER JOIN modelo ON veiculo.IdModelo = modelo.IdModelo";
                SqlDataReader rdr;
                con.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ClienteDomain clienteAluguel = new ClienteDomain()
                        { 
                            nomeCliente = rdr[6].ToString(),
                            sobrenomeCliente = rdr[7].ToString(),         
                        };

                        ModeloDomain modeloCarro = new ModeloDomain();
                        modeloCarro.nomeModelo = rdr[9].ToString();

                        VeiculoDomain veiculoAluguel = new VeiculoDomain()
                        {        
                            placaVeiculo = rdr[8].ToString(), 
                            modelo = modeloCarro
                        };

                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            IdAluguel = Convert.ToInt32(rdr[0]),
                            IdCliente = Convert.ToInt32(rdr[1]),
                            IdVeiculo = Convert.ToInt32(rdr[2]),
                            valorAluguel = Convert.ToDecimal(rdr[3]),
                            dataInicio = Convert.ToDateTime(rdr[4]),
                            dataFim = Convert.ToDateTime(rdr[5]),
                            cliente = clienteAluguel,
                            veiculo = veiculoAluguel
                        };

                        listaAlugueis.Add(aluguel);
                    }
                }
                return listaAlugueis;
            }
        }
    }
}
