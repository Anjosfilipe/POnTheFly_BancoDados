using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POnTheFly
{
    public class Passageiro
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimaCompra { get; set; }
        public char Situacao { get; set; }

        public Passageiro()
        {
        }
        public Passageiro(string Nome, string Cpf, DateTime DataNascimento, char Sexo, DateTime UltimaCompra, DateTime DataCadastro, char Situacao)
        {
            this.Nome = Nome;
            this.Cpf = Cpf;
            this.DataNascimento = DataNascimento;
            this.Sexo = Sexo;
            this.UltimaCompra = DateTime.Now;
            this.DataCadastro = DateTime.Now;
            this.Situacao = 'A';
        }

        public bool ReadCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        public Passageiro CadastrarPassageiro()
        {
            string cpf;
            bool validacao = false;
            DateTime nascimento = new();
            char sexo;
            int opcao = 0;

            Console.Clear();

            Console.WriteLine("Formulário de cadastro:\n");

            Console.Write("Informe seu nome completo: ");
            string nome = Console.ReadLine().ToUpper();

            do
            {
                Console.Write("Digite o numero do seu CPF: ");
                cpf = Console.ReadLine();
                validacao = false;

                if (!ReadCPF(cpf))
                {
                    Console.WriteLine("\nCPF inválido!\n");
                    validacao = true;
                }

            } while (validacao);

            do
            {
                Console.Write("Informe sua data de nascimento: ");
                try
                {
                    nascimento = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nParametro digitado é inválido!");
                    Console.WriteLine("Formato correto: (dd/mm/yyyy)\n");
                    validacao = true;
                }

            } while (validacao);

            Console.WriteLine("Escolha uma das opções abaixo:");
            Console.WriteLine("\n1 - F");
            Console.WriteLine("2 - M");
            Console.WriteLine("3 - Prefiro não me identificar");
            do
            {
                Console.Write("\nOpção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nParametro informado é inválido!\n");
                    validacao = true;
                }

                if (opcao < 1 || opcao > 3)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nEscolha uma das opções apresentadas!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 1)
            {
                sexo = 'F';
            }
            else
            {
                if (opcao == 2)
                {
                    sexo = 'M';
                }

                else
                {
                    sexo = 'N';
                }
            }

            Console.WriteLine("\nCadastro realizado com sucesso!");

            return new Passageiro(nome, cpf, nascimento, sexo, DateTime.Now, DateTime.Now, Situacao);
        }
        public Passageiro LocalizarPassageiro(BancoDados conn, SqlCommand cmd)
        {
            string op = "-1";
            string msg = "";
            string inputCpf;
            string[] options = new string[] { "1", "0" };
            Passageiro passageiro = new();


            
                Console.Clear();
                Console.WriteLine("Localização de passageiro: ");
                Console.WriteLine("\nInforme a operação desejada: ");
                Console.WriteLine("\n01. Localizar");
                Console.WriteLine("00. Voltar");
                Console.Write("\n{0}{1}{2}", msg == "" ? "" : ">>> ", msg, msg == "" ? "" : "\n");
                Console.Write("Opcao: ");
                op = Console.ReadLine();
                cmd = new();
                cmd.Connection = conn.OpenConexao();
                switch (op)
                {
                    case "1":
                        int contador = 0;
                        Console.Write("\nInforme o cpf do passageiro: ");
                        inputCpf = Console.ReadLine();

                        cmd.CommandText = "SELECT * FROM  Passageiro WHERE CPF = @CPF";
                        cmd.Parameters.Add(new SqlParameter("@CPF", inputCpf));

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


                        }

                        cmd.CommandText = "SELECT * FROM  Passageiro WHERE CPF = @CPF1";
                        cmd.Parameters.Add(new SqlParameter("@CPF1", inputCpf));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.Clear();
                            while (reader.Read())
                            {
                                Console.WriteLine("Passageiro");
                                Console.WriteLine("Nome:  {0}", reader.GetString(0));
                                Console.WriteLine("CPF:  {0}", reader.GetString(1));
                                Console.WriteLine("DataNascimento: {0}", reader.GetString(2));
                                Console.WriteLine("Sexo: {0}", reader.GetString(3));
                                Console.WriteLine("UltimaCompra: {0}", reader.GetString(4));
                                Console.WriteLine("DataCadatro: {0}", reader.GetString(5));
                                Console.WriteLine("Situacao: {0}", reader.GetString(5));
                                passageiro.Cpf = reader.GetString(1);
                            }
                        }
                        Console.WriteLine("\nPressione enter para continuar!");
                        Console.ReadKey();
                        break;
                       

                    case "0":
                      
                        break;
                }
            
                return passageiro;
        }
        public void EditarPassageiro(BancoDados conn, SqlCommand cmd)
        {
            bool validacao = false;
           
            int opcao = 10;
            Passageiro passageiro = new();
            Passageiro pas = passageiro.LocalizarPassageiro(conn,cmd);

            do
            {
                Console.Clear();

                Console.WriteLine("Informe qual dado deseja alterar: ");
                Console.WriteLine("\n1 - Nome");
                Console.WriteLine("2 - Data de Nascimento");
                Console.WriteLine("3 - Sexo");
                Console.WriteLine("4 - Situação");
                Console.WriteLine("0 - Sair");
                Console.Write("\nOpção: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    validacao = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nParametro de entrada inválido!\n");
                    validacao = true;
                }

                if (opcao < 0 || opcao > 4)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nEscolha uma das opções apresentadas!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            if (opcao == 0)
            {
                Console.WriteLine("\nPressione enter para voltar ao menu anterior!");
                return;
            }

            else
            {
                if (opcao == 1)
                {
                    Console.Write("Informe o nome do cliente: ");
                    string nome = Console.ReadLine();

                    passageiro.Nome = nome;

                    cmd.CommandText = "UPDATE  Passageiro SET Nome = @Nome WHERE CPF = @CPF7";

                    cmd.Parameters.Add(new SqlParameter("@CPF7", pas.Cpf));
                    cmd.Parameters.Add(new SqlParameter("@Nome", passageiro.Nome));

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\nAlterado com sucesso!");
                }

                else if (opcao == 2)
                {
                    Console.Write("Informe a data de nascimento: ");
                    DateTime nascimento = DateTime.Parse(Console.ReadLine());

                    passageiro.DataNascimento = nascimento;

                    cmd.CommandText = "UPDATE  Passageiro SET DataNascimento = @DataNascimento WHERE CPF = @CPF6";

                    cmd.Parameters.Add(new SqlParameter("@CPF6", pas.Cpf));
                    cmd.Parameters.Add(new SqlParameter("@DataNascimento", passageiro.DataNascimento.ToShortDateString()));

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\nAlterado com sucesso!");
                }

                else
                {
                    if (opcao == 3)
                    {
                        Console.WriteLine("Escolha uma das opções: ");
                        Console.WriteLine("\n1 - F");
                        Console.WriteLine("2 - M");
                        Console.WriteLine("3 - Prefiro não me identificar");
                        do
                        {
                            Console.Write("\nOpção: ");
                            try
                            {
                                opcao = int.Parse(Console.ReadLine());
                                validacao = false;
                            }

                            catch (Exception)
                            {
                                Console.WriteLine("\nParametro informado é inválido!\n");
                                validacao = true;
                            }

                            if (opcao < 1 || opcao > 3)
                            {
                                if (!validacao)
                                {
                                    Console.WriteLine("\nEscolha uma das opções apresentadas!\n");
                                    validacao = true;
                                }
                            }

                        } while (validacao);

                        if (opcao == 1)
                        {
                            passageiro.Sexo = 'F';
                            cmd.CommandText = "UPDATE  Passageiro SET Sexo = @Sexo WHERE CPF = @CPF5";

                            cmd.Parameters.Add(new SqlParameter("@CPF5", pas.Cpf));
                            cmd.Parameters.Add(new SqlParameter("@Sexo", passageiro.Sexo));
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("\nAlterado com sucesso!");
                        }
                        else
                        {
                            if (opcao == 2)
                            {
                                passageiro.Sexo = 'M';
                                cmd.CommandText = "UPDATE  Passageiro SET Sexo = @Sexo WHERE CPF = @CPF4";

                                cmd.Parameters.Add(new SqlParameter("@CPF4", pas.Cpf));
                                cmd.Parameters.Add(new SqlParameter("@Sexo", passageiro.Sexo));
                                cmd.ExecuteNonQuery();
                            }

                            else
                            {
                                passageiro.Sexo = 'N';
                                cmd.CommandText = "UPDATE Passageiro SET Sexo = @Sexo WHERE CPF = @CPF3";

                                cmd.Parameters.Add(new SqlParameter("@CPF3", pas.Cpf));
                                cmd.Parameters.Add(new SqlParameter("@Sexo", passageiro.Sexo));
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    else
                    {
                        if (passageiro.Situacao == 'A')
                        {
                            Console.WriteLine("Deseja alterar a situação do passageiro para inativo: ");
                            Console.WriteLine("\n1 - Sim\n2 - Não");
                            do
                            {
                                Console.Write("\nOpção: ");

                                try
                                {
                                    opcao = int.Parse(Console.ReadLine());
                                    validacao = false;
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine("\nParametro de dado inválido!\n");
                                    validacao = true;
                                }

                                if (opcao < 1 || opcao > 2)
                                {
                                    if (!validacao)
                                    {
                                        Console.WriteLine("\nEscolha uma das opções listadas!\n");
                                        validacao = true;
                                    }
                                }

                            } while (validacao);

                            if (opcao == 1)
                            {
                                passageiro.Situacao = 'I';
                                cmd.CommandText = "UPDATE  Passageiro SET Situacao = @Situacao WHERE CPF = @CPF2";

                                cmd.Parameters.Add(new SqlParameter("@CPF2", pas.Cpf));
                                cmd.Parameters.Add(new SqlParameter("@Situacao", passageiro.Situacao));
                                cmd.ExecuteNonQuery();
                            }

                            else
                            {
                                return;
                            }

                        }

                        else
                        {
                            Console.WriteLine("Deseja alterar a situação do passageiro para ativo: ");
                            Console.WriteLine("\n1 - Sim\n2 - Não");
                            do
                            {
                                Console.Write("\nOpção: ");

                                try
                                {
                                    opcao = int.Parse(Console.ReadLine());
                                    validacao = false;
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine("\nParametro de dado inválido!\n");
                                    validacao = true;
                                }

                                if (opcao < 1 || opcao > 2)
                                {
                                    if (!validacao)
                                    {
                                        Console.WriteLine("\nEscolha uma das opções listadas!\n");
                                        validacao = true;
                                    }
                                }

                            } while (validacao);

                            if (opcao == 1)
                            {
                                passageiro.Situacao = 'A';
                                cmd.CommandText = "UPDATE  Passageiro SET Situacao = @Situacao WHERE CPF = @CPF1";

                                cmd.Parameters.Add(new SqlParameter("@CPF1", pas.Cpf));
                                cmd.Parameters.Add(new SqlParameter("@Situacao", passageiro.Situacao));
                                cmd.ExecuteNonQuery();
                            }

                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
        public void ImprimirPassageiro(BancoDados conn, SqlCommand cmd)
        {
            Console.Clear();

            Passageiro pass = new();
            pass.LocalizarPassageiro(conn, cmd);
        }
        public void AcessarPassageiro(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            Passageiro passageiro = new();
            cmd.Connection = conn.OpenConexao();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR PASSAGEIROS\n");

                Console.WriteLine("1 - Cadastrar Passageiro");
                Console.WriteLine("2 - Editar Passageiro");
                Console.WriteLine("3 - Localizar Passageiro");
                Console.WriteLine("4 - Imprimir Passageiro");
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
                        Passageiro pass = new();
                        pass = passageiro.CadastrarPassageiro();
                        cmd.Connection = conn.OpenConexao();

                        cmd.CommandText = $"Insert into Passageiro (CPF, Nome, DataNascimento, DataCadatro,Sexo,Situacao,UltimaCompra) Values ('{pass.Cpf}', " +
                        $"'{pass.Nome}', '{pass.DataNascimento.ToShortDateString()}', '{pass.DataCadastro.ToShortDateString()}', '{pass.Sexo}', '{pass.Situacao}', '{pass.UltimaCompra.ToShortDateString()}');";

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\t\t\t\t>>>>>>>> CADASTRO REALIZADO COM SUCESSO! <<<<<<<<<<<<");
                        Console.ReadKey();

                        break;

                    case 2:
                        passageiro.EditarPassageiro(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 3:
                        passageiro.LocalizarPassageiro(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 4:
                        passageiro.ImprimirPassageiro(conn, cmd);
                        Console.ReadKey();
                        break;
                }
            } while (opcao != 9);

        }
        public string getData()
        {
            return $"{Nome.PadRight(50)}{Cpf.PadRight(11).Replace(".", string.Empty).Replace("-", string.Empty)}{DataNascimento.ToString("ddMMyyyy")}{Sexo}{UltimaCompra.ToString("ddMMyyyy")}{DataCadastro.ToString("ddMMyyyy")}{Situacao}";
        }
        public override string ToString()
        {
            return ($"Nome: {this.Nome}\nCPF: {this.Cpf}\nSexo: {this.Sexo}\nDada de Nascimento: {this.DataNascimento}\nÚltima Compra: {this.UltimaCompra}\nData do Cadastro: {this.DataCadastro}\nSituação do Cadastro: {this.Situacao}\n");
        }
    }
}