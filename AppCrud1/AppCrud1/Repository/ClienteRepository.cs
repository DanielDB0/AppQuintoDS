using AppCrud1.Models;
using AppCrud1.Repository.Contract;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System.Data;

namespace AppCrud1.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Cliente Cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call spAttCli(@IdCli, @nomeCli, @CEP, @Telefone)", conexao);



                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = Cliente.NomeCli;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = Cliente.Telefone;
                cmd.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = Cliente.CEP;
                cmd.Parameters.Add("@IdCli", MySqlDbType.VarChar).Value = Cliente.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void Cadastrar(Cliente Cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call spCadastrarCli(@nomeCli, @CEP, @Telefone)", conexao);

                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = Cliente.NomeCli;
                cmd.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = Cliente.CEP;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = Cliente.Telefone;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call spExcluirCli(@IdCli)", conexao);
                cmd.Parameters.AddWithValue("@IdCli", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> ClienteList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {

                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call spObterAllClients", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    ClienteList.Add(
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["IdCli"]),
                            NomeCli = (string)dr["NomeCli"],
                            CEP = Convert.ToInt32(dr["CEP"]),
                            Telefone = Convert.ToInt64(dr["Telefone"]),
                            DataCadastrada = Convert.ToDateTime(dr["DataRegistro"])
                        });
                }
                return ClienteList;


            }
        }

        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {

                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call spObterClient(@IdCli)", conexao);

                cmd.Parameters.AddWithValue("@IdCli", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Cliente Cliente = new Cliente();

                while (dr.Read())
                {
                    Cliente.Id = Convert.ToInt32(dr["IdCli"]);
                    Cliente.NomeCli = (string)dr["NomeCli"];
                    Cliente.CEP = Convert.ToInt32(dr["CEP"]);
                    Cliente.Telefone = Convert.ToInt64(dr["Telefone"]);
                    Cliente.DataCadastrada = Convert.ToDateTime(dr["DataRegistro"]);
                }
                return Cliente;

            }

        }
    }
}
