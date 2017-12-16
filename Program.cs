using System;
using System.Data;
using System.Data.SqlClient;  

namespace SqlDataAdapterExample
{
    class Program
    {
        #region Comandos SQL

        private static string SQL_SELECT = "SELECT * FROM CLIENTES";
        private static string SQL_INSERT = "INSERT INTO CLIENTES (Nome, Email) VALUES ('Richiely Paiva', 'richiely@email.com')";

        #endregion

        #region Métodos Públicos

        public static void Main(string[] args)
        {
            SqlConnection conexao = CrieConexao();
            var selectCmd = new SqlCommand(SQL_SELECT, conexao);
            // representa o banco de dados
            var dataSet = new DataSet("CADASTRO");

            try
            {
                conexao.Open();

                // O SqlDataAdapter faz a ponte com o SQL e o objeto DataSet, 
                // preenchendo -o assim, com as informações recuperadas pelo comando executado
                var adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCmd;

                // utilize os comandos que o adapeter oferece - Select, Insert, Delete, Update - para manipular os dados
                //adapter.InsertCommand = new SqlCommand(SQL_INSERT, conexao);
                //adapter.InsertCommand.ExecuteNonQuery();

                // preenche a tabela
                adapter.Fill(dataSet, "CLIENTES");

                SalveDadosXML(dataSet);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }

        }

        #endregion

        #region Métodos Privados

        private static void SalveDadosXML(DataSet dataSet)
        {
            var dt = dataSet.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("ID Cliente: " + row[0].ToString());
                Console.WriteLine("Nome Cliente: " + row[1].ToString());
                Console.WriteLine("EMail: " + row[2].ToString());
            }
            dt.WriteXml("dados.xml");
        }

        private static SqlConnection CrieConexao()
        {
            var constr = @"Data Source = (localdb)\v11.0; Initial Catalog = CADASTRO; Integrated Security = True; " +
                            "Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            return new SqlConnection(constr);
        }

        #endregion
    }
}
