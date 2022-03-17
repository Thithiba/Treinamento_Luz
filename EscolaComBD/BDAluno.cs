using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjetoEscola
{
    public class BDAluno
    {
        string stringConexao;
        NpgsqlCommand cmd = new NpgsqlCommand();
        public NpgsqlConnection conexao = new NpgsqlConnection();

        public BDAluno()
        {
            stringConexao = "Server = localhost; Port = 5432; Database = eschola; User ID = postgres;  Password = Thiba123$;";
            conexao = new NpgsqlConnection();
            conexao.ConnectionString = stringConexao;
            cmd.Connection = conexao;
        }

        public List<Usuario> AlunoList(List<Usuario>usuarioList)
        {
            NpgsqlDataReader rdr;
            cmd.CommandText = "Select * from usuario inner join aluno on usuario.usuario_id = aluno.usuario_id";

            try
            {
                conexao.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    usuarioList.Add(new Aluno()
                    {
                        Id = rdr.GetInt32(0),
                        Tipo = rdr.GetString(1),
                        Name = rdr.GetString(2),
                        Rg = rdr.GetString(3),
                        Cpf = rdr.GetString(4),
                        Data = rdr.GetString(5),
                        Aluno_Id = rdr.GetInt32(6),
                        NamePai = rdr.GetString(8),
                        TelPai = rdr.GetString(9),
                        NameMae = rdr.GetString(10),
                        TelMae = rdr.GetString(11),
                        Turma = rdr.GetString(12),
                    });
                }
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

        public bool InsereAluno(int usuario_id, string nomepai, string num_pai, string nomemae, string tel_mae, string turma)
        {
            conexao.Open();
            cmd.Connection = conexao;
            cmd.CommandText = $"INSERT INTO aluno (usuario_id, nome_pai, tel_pai, nome_mae, tel_mae, turma)" +
                $" VALUES ({usuario_id},'{nomepai}', '{num_pai}', '{nomemae}', '{tel_mae}', '{turma}')";
            int res = cmd.ExecuteNonQuery();

            if (res == -1)
            {
                return false;
            }
            else
            {
                MessageBox.Show("Aluno inserido com sucesso");
                return true;
            }
        }
        public bool AtualizaAluno(int id, string nomepai, string num_pai, string nomemae, string tel_mae, string turma)
        {
            conexao.Open();
            cmd.Connection = conexao;
            cmd.CommandText = $"UPDATE aluno set nome_pai = '{nomepai}', tel_pai = '{num_pai}', nome_mae = '{nomemae}', tel_mae = '{tel_mae}', turma = '{turma}' where aluno_id = {id}";
            try
            {
                int res = cmd.ExecuteNonQuery();
                MessageBox.Show("Aluno alterado com sucesso");
            }
            catch 
            {
              
            }
            finally
            {
                conexao.Close();
            }
            return true;
        }

        public bool DeletaAluno(int id)
        {  
            try
            {
                conexao.Open();
                cmd.Connection = conexao;
                cmd.CommandText = $"delete from aluno where aluno_id = {id}";
                int res = cmd.ExecuteNonQuery();
                MessageBox.Show("Aluno removido com sucesso");
            }
            catch 
            {
                
            }
            finally
            {
                conexao.Close();
            }
            return true;
        }
    }
}
