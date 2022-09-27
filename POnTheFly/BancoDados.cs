using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POnTheFly
{
    public class BancoDados
    {
        string Conexao = "Data Source = localhost; Initial Catalog = OnTheFly; User Id = sa; Password = Tricolor2511;";

        public BancoDados()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }

        public SqlConnection OpenConexao()
        {
            BancoDados conn = new BancoDados();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            return conexaosql;
        }

        public SqlConnection CloseConexao()
        {
            BancoDados conn = new BancoDados();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Close();
            return conexaosql;
        }
    }
}
