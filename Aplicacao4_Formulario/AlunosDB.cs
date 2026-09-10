using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Npgsql;

namespace Aplicacao4_Formulario
{
    public class AlunosDB
    {
        private string conexao;

        public AlunosDB()
        {
            conexao = ConfigurationManager.ConnectionStrings["ConexaoBancoDados"].ConnectionString;
        }

        // Método para incluir aluno
        public void IncluirAluno(Alunos alunos)
        {
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(conexao);

            try
            {
                using (pgsqlConnection)
                {
                    pgsqlConnection.Open();

                    string cmdInserir = String.Format(
                        "INSERT INTO public.informacoes(matricula, nome, n1, n2, media, status) " +
                        "VALUES({0}, '{1}', {2}, {3}, {4}, '{5}')",
                        alunos.Matricula,
                        alunos.Nome.Replace("'", "''"), // Evita SQL Injection
                        alunos.N1.ToString().Replace(",", "."),
                        alunos.N2.ToString().Replace(",", "."),
                        alunos.Media.ToString().Replace(",", "."),
                        alunos.Status
                    );

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        // Método para listar alunos
        public DataTable getAlunos()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(conexao);

            try
            {
                using (pgsqlConnection)
                {
                    pgsqlConnection.Open();
                    string cmdSeleciona = "SELECT id, matricula, nome, n1, n2, media, status, data_cadastro FROM public.informacoes ORDER BY id DESC";

                    using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                    {
                        Adpt.Fill(dt);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }

            return dt;
        }
    }
}