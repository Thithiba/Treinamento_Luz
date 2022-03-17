using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjetoEscola
{
    public class BDUsuario
    {
        string stringConexao;
        NpgsqlCommand cmd = new NpgsqlCommand();
        public NpgsqlConnection conexao = new NpgsqlConnection();
        private BDAluno bd_aluno;
        private BDProfessor bd_professor;

        public BDUsuario()
        {
            stringConexao = "Server = localhost; Port = 5432; Database = eschola; User ID = postgres;  Password = Thiba123$;";
            conexao = new NpgsqlConnection();
            conexao.ConnectionString = stringConexao;
            cmd.Connection = conexao;
            bd_aluno = new BDAluno();
            bd_professor = new BDProfessor();
        }

        public List<Usuario> GetTodosRegistrosUsuario(List<Usuario>listaUsuario, BDAluno nomegenerico, BDProfessor nomegenerico2)
        {
            nomegenerico.AlunoList(listaUsuario);
            nomegenerico2.ProfessorList(listaUsuario);
            return listaUsuario;
        }

        public int InsereUsuario(string tipo, string nome, string rg, string cpf, string data_nasc)
        {
            conexao.Open();
            cmd.Connection = conexao;
            cmd.CommandText = $"INSERT INTO usuario (tipo, nome, rg, cpf, data_nasc)" +
                $" VALUES ('{tipo}', '{nome}', '{rg}', '{cpf}', '{data_nasc}') returning usuario_id";
            try
            {
                int userID = (int)cmd.ExecuteScalar();
                //MessageBox.Show("Usuario inserido com sucesso");
                return userID;
            }
            catch
            {
                return 0;
            }
            finally
            {
                conexao.Close();
            }
        }
        public void AtualizaUsuario(int id, string nome, string rg, string cpf, string data_nasc)
        {
            conexao.Open();
            cmd.Connection = conexao;
            cmd.CommandText = $"UPDATE usuario set nome = '{nome}', rg = '{rg}', cpf = '{cpf}', data_nasc = '{data_nasc}' where usuario_id = {id}";
            try
            {
                int res = cmd.ExecuteNonQuery();
                //MessageBox.Show("Usuario alterado com sucesso");
            }
            catch
            {
                
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletaUsuario(int id)
        {
            try
            {
                conexao.Open();
                cmd.Connection = conexao;
                cmd.CommandText = $"delete from usuario where usuario_id = {id}";
                int res = cmd.ExecuteNonQuery();
                //MessageBox.Show("Usuario removido com sucesso");
            }
            catch
            {
                
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}