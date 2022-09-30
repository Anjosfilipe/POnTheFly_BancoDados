using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POnTheFly
{
    public class Aeronave
    {
        public string Inscricao { get; set; }
        public string Tipo { get; set; }
        public string Capacidade { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public Aeronave()
        {
        }
        public Aeronave(string inscricao, string capacidade, string acentosOcupado, DateTime ultimaVenda, DateTime dataCadastro, char situacao)
        {
            Inscricao = inscricao;
            Tipo = "Comercial";
            Capacidade = capacidade;
            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }
        public Aeronave Cadastrar()
        {
            int opcao = 0, capacidade = 0;
            bool condicaoDeParada;
            string inscricao, codigoInscricao;


            Console.Clear();

            Console.WriteLine("Vamos iniciar o cadastro de sua aeronave.\n");

            do
            {
                do
                {
                    Console.WriteLine("Qual o código inicial da inscrição da Aeronave:  ");
                    Console.WriteLine("\n1 - PT\n2 - PP\n3 - PR\n4 - PU\n");
                    Console.Write("Opção: ");

                    try
                    {
                        opcao = int.Parse(Console.ReadLine());
                        condicaoDeParada = false;
                    }

                    catch (Exception)
                    {
                        Console.WriteLine("\nParametro informado é inválido!\n");
                        condicaoDeParada = true;
                    }

                    if (opcao < 1 || opcao > 4)
                    {
                        if (!condicaoDeParada)
                        {
                            Console.WriteLine("\nParametro informado é inválido!\n");
                            condicaoDeParada = true;
                        }
                    }

                } while (condicaoDeParada);

                do
                {
                    Console.Write("\nInforme a inscrição da Aeronave sem o código: ");

                    inscricao = Console.ReadLine().ToUpper();
                    condicaoDeParada = false;

                    if (inscricao.Length != 3)
                    {
                        Console.WriteLine("\nInscrição sem o código deve ter 3 letras!\n");
                        condicaoDeParada = true;
                    }

                } while (condicaoDeParada);

                if (opcao == 1)
                {
                    codigoInscricao = "PT" + inscricao;
                }

                else if (opcao == 2)
                {
                    codigoInscricao = "PP" + inscricao;
                }

                else
                {
                    if (opcao == 3)
                    {
                        codigoInscricao = "PR" + inscricao;
                    }

                    else
                    {
                        codigoInscricao = "PS" + inscricao;
                    }
                }


            } while (condicaoDeParada);

            do
            {
                Console.Write("Qual a capacidade de passageiros da Aeronave: ");

                try
                {
                    capacidade = int.Parse(Console.ReadLine());
                    condicaoDeParada = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nParametro de entráda inválido!\n");
                    condicaoDeParada = true;
                }

                if (capacidade < 0)
                {
                    Console.WriteLine("\nCapacidade da Aeronave, não pode ser menor que 0\n");
                    condicaoDeParada = true;
                }

            } while (condicaoDeParada);

            string strCapacidade = "" + capacidade;
            string acentosOcupados = "000";

            Console.WriteLine("\nAeronave cadastrada com sucesso!");

            return new Aeronave(codigoInscricao, strCapacidade, acentosOcupados, DateTime.Now, DateTime.Now, 'A');
        }
        public Aeronave Localizar(BancoDados conn, SqlCommand cmd)
        {
            string inscricao;
            int contador = 0;
            Aeronave aeronave = new Aeronave();


            Console.Clear();
            Console.WriteLine("Olá,\n");

            Console.Write("Informe a incrição da Aeronave com codigo: ");
            inscricao = Console.ReadLine().ToUpper();

            cmd = new();
            cmd.Connection = conn.OpenConexao();

            cmd.CommandText = "SELECT * FROM  Aeronave WHERE Inscricao = @Inscricao0";
            cmd.Parameters.Add(new SqlParameter("@Inscricao0", inscricao));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        contador++;
                    }
                }
            }
            if (contador == 0)
            {
                Console.WriteLine("\nInscrição informado não está cadastrado em nosso banco de dados!");
                Console.WriteLine("Pressione enter apra continuar!");

                return null;
            }

            cmd.CommandText = "SELECT * FROM  Aeronave WHERE Inscricao = @Inscricao1";
            cmd.Parameters.Add(new SqlParameter("@Inscricao1", inscricao));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.Clear();
                while (reader.Read())
                {
                    Console.WriteLine("Aeronave");
                    Console.WriteLine("Inscricao:  {0}", reader.GetString(0));
                    Console.WriteLine("Capacidade:  {0}", reader.GetInt32(1));
                    Console.WriteLine("Ultima venda:  {0}", reader.GetString(2));
                    Console.WriteLine("Data cadastro:  {0}", reader.GetString(3));
                    Console.WriteLine("Situacao:  {0}", reader.GetString(4));
                    Console.WriteLine("CNPJ:  {0}", reader.GetString(5));
                    aeronave.Inscricao = reader.GetString(0);
                    aeronave.Capacidade = reader.GetInt32(1).ToString();
                }
            }
            Console.WriteLine("\nPressione enter para continuar!");
            
            Console.ReadKey();
            return aeronave;
        }
        public void Imprimir(BancoDados conn, SqlCommand cmd)
        {

            Aeronave aero = new Aeronave();
            aero.Localizar(conn, cmd);
        }
        public void Editar(BancoDados conn, SqlCommand cmd)
        {
            int capacidade;

            Aeronave aeronave = new Aeronave();
            Aeronave ar = aeronave.Localizar(conn, cmd);

            Console.Clear();

            Console.WriteLine("\nInforme qual será alteração: \n");

            Console.Write("1 - Capacidade\n2 - Situação\n\nOpcão: ");
            int resposta = int.Parse(Console.ReadLine());

            cmd.Connection = conn.OpenConexao();

            if (resposta == 1)
            {
                do
                {

                    Console.Write("\nInforme qual a capicade da Aeronave: ");
                    capacidade = int.Parse(Console.ReadLine());

                    if (capacidade < 0)
                    {
                        Console.WriteLine("\nCapacidade de passageiros não pode ser menor que 0!");
                    }

                    else
                    {
                        string strCapacidade = "" + capacidade;
                        aeronave.Capacidade = strCapacidade;

                        cmd.CommandText = "UPDATE  Aeronave SET Capacidade = @Capacidade WHERE Inscricao = @Inscricao0";

                        cmd.Parameters.Add(new SqlParameter("@Inscricao0", ar.Inscricao));
                        cmd.Parameters.Add(new SqlParameter("@Capacidade", aeronave.Capacidade));

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nAlterado com sucesso!");

                    }

                } while (capacidade < 0);
            }

            else
            {
                if (aeronave.Situacao == 'A')
                {
                    Console.WriteLine("\nDeseja alterar a situação desta Aeronave para Inativa?");
                    Console.Write("\n1 - Sim\n2 - Não\n\nOpção: ");
                    int opcao = int.Parse(Console.ReadLine());

                    if (opcao == 1)
                    {
                        aeronave.Situacao = 'I';


                        cmd.CommandText = "UPDATE  Aeronave SET Situacao = @Situacao WHERE Inscricao = @Inscricao1";

                        cmd.Parameters.Add(new SqlParameter("@Inscricao", aeronave.Inscricao));
                        cmd.Parameters.Add(new SqlParameter("@Situacao", aeronave.Situacao));

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nAlterado com sucesso!");
                    }

                    else
                    {
                        Console.WriteLine("\nAté logo!");
                    }
                }

                else
                {
                    Console.WriteLine("\nDeseja alterar a situação desta Aeronave para Ativa?");
                    Console.Write("\n1 - Sim\n2 - Não\n\nOpção: ");
                    int opcao = int.Parse(Console.ReadLine());

                    if (opcao == 1)
                    {
                        Console.WriteLine("\nAlterado com sucesso!");
                        aeronave.Situacao = 'A';
                    }

                    else
                    {
                        Console.WriteLine("\nAté logo!");
                    }
                }
            }

        }
        public void AcessarAeronave(BancoDados conn, SqlCommand cmd)
        {
            int opcao = -1;
            bool condicaoDeParada = false;
            Aeronave aeronave = new();
            cmd.Connection = conn.OpenConexao();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR AERONAVES\n");

                Console.WriteLine("1 - Cadastrar Aeronave");
                Console.WriteLine("2 - Editar Aeronave");
                Console.WriteLine("3 - Localizar Aeronave");
                Console.WriteLine("4 - Imprimir Aeronaves");
                Console.WriteLine("\n9 - Voltar ao menu anterior");
                Console.Write("\nOpção: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    condicaoDeParada = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("Parametro de entrada inválido!");
                    Console.WriteLine("Pressione enter para escolher novamente!");
                    Console.ReadKey();
                    condicaoDeParada = true;
                }

                if (opcao < 0 || opcao > 4 && opcao != 9)
                {
                    if (!condicaoDeParada)
                    {
                        Console.WriteLine("Escolha uma das opções disponiveis!!");
                        Console.WriteLine("Pressione enter para escolher novamente!");
                        Console.ReadKey();
                        condicaoDeParada = true;
                    }
                }

                switch (opcao)
                {
                    case 1:

                        Console.Write("Informe o CNPJ da Coompania responsavel da aeronave: ");
                        string cnpj = Console.ReadLine();

                        Aeronave ar = aeronave.Cadastrar();
                        cmd.CommandText = "INSERT INTO Aeronave (Inscricao, Capacidade, UltimaVenda, DataCadastro, Situacao, CNPJ) VALUES (@Inscricao, @Capacidade, @UltimaVenda, @DataCadastro, @SituacaoA, @CNPJ);";

                        SqlParameter Inscricao = new SqlParameter("@Inscricao", System.Data.SqlDbType.VarChar, 6);
                        SqlParameter Capacidade = new SqlParameter("@Capacidade", System.Data.SqlDbType.Int);
                        SqlParameter UltimaVenda = new SqlParameter("@UltimaVenda", System.Data.SqlDbType.VarChar, 10);
                        SqlParameter DataCadastro = new SqlParameter("@DataCadastro", System.Data.SqlDbType.VarChar, 10);
                        SqlParameter Situacao = new SqlParameter("@SituacaoA", System.Data.SqlDbType.Char, 1);
                        SqlParameter CNPJ = new SqlParameter("@CNPJ", System.Data.SqlDbType.VarChar, 11);

                        Inscricao.Value = ar.Inscricao;
                        Capacidade.Value = ar.Capacidade;
                        UltimaVenda.Value = ar.UltimaVenda;
                        DataCadastro.Value = ar.DataCadastro;
                        Situacao.Value = ar.Situacao;
                        CNPJ.Value = cnpj;

                        cmd.Parameters.Add(Inscricao);
                        cmd.Parameters.Add(CNPJ);
                        cmd.Parameters.Add(Capacidade);
                        cmd.Parameters.Add(UltimaVenda);
                        cmd.Parameters.Add(DataCadastro);
                        cmd.Parameters.Add(Situacao);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\t\t\t\t>>>>>>>> CADASTRO REALIZADO COM SUCESSO! <<<<<<<<<<<<");
                        Console.ReadKey();
                        break;

                    case 2:
                        aeronave.Editar(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("\t\t\t\t>>>>>>>> AERONAVE LOCALIZADA COM SUCESSO! <<<<<<<<<<<<");
                        aeronave.Localizar(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 4:
                        aeronave.Imprimir(conn, cmd);
                        Console.ReadKey();
                        break;
                }

                cmd.Connection = conn.CloseConexao();
            } while (opcao != 9);
        }
        public string getData()
        {
            return $"{Inscricao}{Capacidade.PadRight(3)}{UltimaVenda.ToString("ddMMyyyy")}{DataCadastro.ToString("ddMMyyyy")}{Situacao}";
        }
        public override string ToString()
        {
            return $"Inscrição: {Inscricao}\nCapacidade: {Capacidade} Passageiros\nData da ultima venda: {UltimaVenda.ToShortDateString()}\nData do Cadastro: {DataCadastro.ToLongDateString()}\nSituação: {Situacao}\n";
        }
    }
}
