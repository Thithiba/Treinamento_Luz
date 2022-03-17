using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjetoEscola
{
    public class BDProfessor
    {
        string stringConexao;
        NpgsqlCommand cmd = new NpgsqlCommand();
        public NpgsqlConnection conexao = new NpgsqlConnection();
        public BDProfessor()
        {
            stringConexao = "Server = localhost; Port = 5432; Database = eschola; User ID = postgres;  Password = Thiba123$;";
            conexao = new NpgsqlConnection();
            conexao.ConnectionString = stringConexao;
            cmd.Connection = conexao;
        }

        public List<Usuario> ProfessorList(List<Usuario>usuarioList)
        {
            conexao.Open();
            NpgsqlDataReader rdr;
            cmd.CommandText = "Select * from usuario inner join professor on usuario.usuario_id = professor.usuario_id";
            try
            {
                
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    usuarioList.Add(new Professor()
                        {
                            Id = rdr.GetInt32(0),
                            Tipo = rdr.GetString(1),
                            Name = rdr.GetString(2),
                            Rg = rdr.GetString(3),
                            Cpf = rdr.GetString(4),
                            Data = rdr.GetString(5),
                            Prof_Id = rdr.GetInt32(6),
                            Contato = rdr.GetString(8),
                            TelContato = rdr.GetString(9),
                            Email = rdr.GetString(10),
                            Materia = rdr.GetString(11),
                        });
            }
            catch 
            {
                MessageBox.Show("Não foi possivel conectar");
            }
            finally
            {
                conexao.Close();
            }
            return usuarioList;
        }

        public bool InsereProfessor(int usuario_id , string nome_contato, string tel_contato, string email, string materia)
        {
            conexao.Open();
            cmd.Connection = conexao;
            cmd.CommandText = $"INSERT INTO professor (usuario_id, nome_contato, tel_contato, email, materia)" +
                $" VALUES ({usuario_id}, '{nome_contato}', {tel_contato}, '{email}', '{materia}')";
            int res = cmd.ExecuteNonQuery();

            if (res == -1)
            {
                return false;
            }
            else
            {
                MessageBox.Show("Professor inserido com sucesso");
                return true;
            }
        }
        public bool AtualizaProfessor(int id, string nome_contato, string tel_contato, string email, string materia)
        {
            
            try
            {
                conexao.Open();
                cmd.Connection = conexao;
                cmd.CommandText = $"UPDATE professor set nome_contato = '{nome_contato}', tel_contato = '{tel_contato}', email = '{email}', materia = '{materia}' where professor_id = {id}";
                int res = cmd.ExecuteNonQuery();
                MessageBox.Show("Professor alterado com sucesso");
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar");
            }
            finally
            {
                conexao.Close();
            }
            return true;
        }

        public bool DeletaProfessor(int id)
        {
            try
            {
                conexao.Open();
                cmd.Connection = conexao;
                cmd.CommandText = $"delete from professor where professor_id = {id}";
                int res = cmd.ExecuteNonQuery();
                MessageBox.Show("Professor removido com sucesso");
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar");
            }
            finally
            {
                conexao.Close();
            }
            return true;
        }
    }
}