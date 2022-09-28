using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POnTheFly
{
    public class Voo
    {
        public int IDVoo { get; set; }
        public string AcentosOcupado { get; set; }
        public string Destino { get; set; }
        public string InscricaoAeronave { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }



        public Voo()
        {
        }
        public Voo(int iDVoo, string destino, string aeronave, DateTime dataVoo, DateTime dataCadastro, char situacao)
        {
            IDVoo = iDVoo;
            Destino = destino;
            InscricaoAeronave = aeronave;
            DataVoo = dataVoo;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public Voo CadastrarVoo(BancoDados conn, SqlCommand cmd)
        {
            Voo voo = new Voo();
            int contador = 0;
            DateTime dataVoo = DateTime.Now;
            bool condicaoDeSaida = false;




            if (voo.IDVoo > 9999)
            {
                Console.WriteLine("Numero maximo de voo cadastrados");
                return null;
            }

            else
            {
                Console.Clear();


                Console.Write("Informe o destino do voo: ");
                string destino = Console.ReadLine().ToUpper();

                //// ---- validação da IATA do voo ----- //// 

                cmd = new();
                cmd.Connection = conn.OpenConexao();

                cmd.CommandText = "SELECT * FROM IATA WHERE IATA = @IATA";
                cmd.Parameters.Add(new SqlParameter("IATA", destino));
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
                    Console.WriteLine("\nIATA informado não está cadastrado em nosso banco de dados!");
                    Console.WriteLine("Pressione enter apra continuar!");

                    return null;
                }
                Console.Write("Informe a incrição da aeronave desejada: ");
                string IdAeronave = Console.ReadLine().ToUpper();

                cmd.CommandText = "SELECT * FROM Aeronave WHERE Inscricao = @Inscricao";
                cmd.Parameters.Add(new SqlParameter("@Inscricao", IdAeronave));
               
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
                    Console.WriteLine("\nAERONAVE informado não está cadastrado em nosso banco de dados!");
                    Console.WriteLine("Pressione enter apra continuar!");

                    return null;
                }

                else
                {
                    // ----- continua cadastro 


                    do
                    {
                        Console.Write("Infome a data e hora do voo: ");

                        try
                        {
                            dataVoo = DateTime.Parse(Console.ReadLine());
                            condicaoDeSaida = false;
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("\nData e hora informada deve seguir o formato apresentado: (dd/mm/aaaa) (hh:mm)\n");
                            condicaoDeSaida = true;
                        }

                        if (dataVoo < DateTime.Now)
                        {
                            if (!condicaoDeSaida)
                            {
                                Console.WriteLine("\nNão é possivel comprar passagem com data e a hora retroativa!\n");
                                condicaoDeSaida = true;
                            }
                        }

                    } while (condicaoDeSaida);

                    Console.WriteLine("\nCadastro Realizado com sucesso!");

                    return new Voo(contador, destino, IdAeronave, dataVoo, DateTime.Now, 'A');
                }
            }
        }
        public Voo LocalizarVoo(BancoDados conn, SqlCommand cmd)
        {
            int id = 0;
            int contador = 0;
            Voo v = new Voo();

            Console.Clear();

            Console.Write("Informe o id do Voo com 4 digitos numérios: ");
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("\nParametro de entrada é inválido!");
                return null;
            }


            cmd = new();
            cmd.Connection = conn.OpenConexao();

            cmd.CommandText = "SELECT * FROM  Voo WHERE ID_Voo = @ID_Voo0";
            cmd.Parameters.Add(new SqlParameter("@ID_Voo0", id));
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
                Console.WriteLine("\nCNPJ informado não está cadastrado em nosso banco de dados!");
                Console.WriteLine("Pressione enter apra continuar!");

                return null;
            }

            cmd.CommandText = "SELECT * FROM  Voo WHERE ID_Voo = @ID_Voo1";
            cmd.Parameters.Add(new SqlParameter("@ID_Voo1", id));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.Clear();
                while (reader.Read())
                {
                    Console.WriteLine("Voo");
                    Console.WriteLine("ID_Voo: {0}", reader.GetString(0));
                    Console.WriteLine("Destino: {0}", reader.GetString(1));
                    Console.WriteLine("Aeronave_Id:  {0}", reader.GetString(2));
                    Console.WriteLine("Data Voo: {0}", reader.GetDateTime(3));
                    Console.WriteLine("Data Cadastro: {0}", reader.GetString(4));
                    Console.WriteLine("Situação: {0}", reader.GetString(5));
                    Console.WriteLine("Assentos Ocupados: {0}", reader.GetInt32(6));

                    v.IDVoo = int.Parse(reader.GetString(0));
                }
            }


            Console.WriteLine("\nPressione enter para continuar!");
            Console.ReadKey();


            return v;
        }
        public void ImprimirVoo(BancoDados conn, SqlCommand cmd)
        {
            Console.Clear();

            Voo voo = new();
            voo.LocalizarVoo(conn, cmd);
        }
        public void EditarVoo(BancoDados conn, SqlCommand cmd)
        {
            Voo v = new Voo();
            Voo test = new();
            int op = 0;

            Console.Clear();

            v = test.LocalizarVoo(conn, cmd);

            Console.WriteLine("Escolha a opção desejada");
            Console.WriteLine("\n1 - Editar Destino");
            Console.WriteLine("2 - Editar Aeronave");
            Console.WriteLine("3 - Editar Data do voo");
            Console.WriteLine("4 - Situação");
            Console.WriteLine("0 - Sair");
            Console.Write("\nOpção: ");
            op = int.Parse(Console.ReadLine());

            if (op == 1)
            {
                Console.WriteLine("\nInforme o id do novo destino:");
                string destino = Console.ReadLine().ToUpper();
                v.Destino = destino;

                cmd.CommandText = "UPDATE  Voo SET Destino = @destino WHERE ID_Voo = @id";

                cmd.Parameters.Add(new SqlParameter("@id", v.IDVoo));
                cmd.Parameters.Add(new SqlParameter("@destino", v.Destino));

                cmd.ExecuteNonQuery();
                Console.WriteLine("\nAlterado com sucesso!");
            }

            else if (op == 2)
            {
                Console.WriteLine("\nInforme a inscrição da nova Aeronave:");
                string aeronave = Console.ReadLine().ToUpper();
                v.InscricaoAeronave = aeronave;

                cmd.CommandText = "UPDATE  Voo SET Aeronave_Id = @aeronaveid WHERE ID_Voo = @id";

                cmd.Parameters.Add(new SqlParameter("@id", v.IDVoo));
                cmd.Parameters.Add(new SqlParameter("@aeronaveid", v.InscricaoAeronave));

                cmd.ExecuteNonQuery();
                Console.WriteLine("\nAlterado com sucesso!");
            }

            else if (op == 3)
            {
                Console.WriteLine("\nInforme a nova data do voo:");
                DateTime data = DateTime.Parse(Console.ReadLine());
                v.DataVoo = data;

                cmd.CommandText = "UPDATE  Voo SET DataVoo = @datavoo WHERE ID_Voo = @id";

                cmd.Parameters.Add(new SqlParameter("@id", v.IDVoo));
                cmd.Parameters.Add(new SqlParameter("@datavoo", v.DataVoo));

                cmd.ExecuteNonQuery();
                Console.WriteLine("\nAlterado com sucesso!");
            }

            else
            {
                if (op == 4)
                {
                    Console.WriteLine("\nInforme o situação:");
                    char situacao = char.Parse(Console.ReadLine().ToUpper());
                    v.Situacao = situacao;

                    cmd.CommandText = "UPDATE  Voo SET Situacao = @Situacao WHERE ID_Voo = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", v.IDVoo));
                    cmd.Parameters.Add(new SqlParameter("@Situacao", v.Situacao));

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\nAlterado com sucesso!");
                }
            }

        }
        public void AcessarVoo(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            Voo voo = new();
           

            cmd.Connection = conn.OpenConexao();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR VOO\n");

                Console.WriteLine("1 - Cadastrar Voo");
                Console.WriteLine("2 - Editar Voo");
                Console.WriteLine("3 - Localizar Voo");
                Console.WriteLine("4 - Imprimir Voo");
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

                if (opcao < 1 || opcao > 4 && opcao != 9)
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
                        Voo voo1 = new Voo();
                        voo1 = voo.CadastrarVoo(conn, cmd);

                        cmd.Connection = conn.OpenConexao();
                        if (voo1 == null)
                        {
                        }

                        else
                        {
                            cmd.CommandText = $"Insert into Voo (ID_Voo, Destino, Aeronave_Id, DataVoo, DataCadastro, Situacao, AssentosOcupados) Values ('{voo1.IDVoo}', " +
                        $"'{voo1.Destino}', '{voo1.InscricaoAeronave}', '{voo1.DataVoo}', '{voo1.DataCadastro.ToShortDateString()}', '{voo1.Situacao}', '{0}');";

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("\t\t\t\t>>>>>>>> CADASTRO REALIZADO COM SUCESSO! <<<<<<<<<<<<");
                            Console.ReadKey();
                        }

                        break;

                    case 2:
                        voo.EditarVoo(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 3:
                        voo.LocalizarVoo(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 4:
                        voo.ImprimirVoo(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 9:
                        cmd.Connection = conn.CloseConexao();
                        Console.WriteLine("Até");
                        break;
                }

            } while (opcao != 9);
        }
        public override string ToString()
        {
            return "\nID Voo:V " + this.IDVoo.ToString("D4") + "\nDestino: " + this.Destino + "\nAeronave: " + this.InscricaoAeronave + "\nData do voo: " + this.DataVoo + "\nData de Cadastro: " + this.DataCadastro + "\nSituação: " + this.Situacao + "\n";
        }
    }
}
