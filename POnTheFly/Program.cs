using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Projeto_OnTheFly;
using System.Data.SqlClient;

namespace POnTheFly
{
    internal class Program
    {

        static void Main(string[] args)
        {

            //Program
            int opcao = 5;
            bool condicaoDeParada;
            BancoDados conn = new BancoDados();
            SqlCommand cmd = new SqlCommand();
            CompanhiaAerea companhia = new CompanhiaAerea();
            Aeronave aeronave = new Aeronave();
            Passageiro passageiro = new Passageiro();
            Voo voo = new Voo();
            PassagemVoo passagem = new PassagemVoo();
            Venda venda = new Venda();

            cmd.Connection = conn.OpenConexao();

            do
            {
                Console.Clear();

                Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                Console.WriteLine("Selecione a opção desejada: ");
                Console.WriteLine("\n1 - Companhias");
                Console.WriteLine("2 - Aeronaves");
                Console.WriteLine("3 - Voo");
                Console.WriteLine("4 - Passagens");
                Console.WriteLine("5 - Vendas");
                Console.WriteLine("6 - Passageiros");
                Console.WriteLine("7 - Bloqueados/Restritos");
                Console.WriteLine("\n0 - Sair");
                Console.Write("\nOpção: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    condicaoDeParada = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nParametro de entrada inválido!");
                    Console.WriteLine("Pressione enter para escolher novamente!");
                    Console.ReadKey();
                    condicaoDeParada = true;
                }

                if (opcao < 0 || opcao > 7)
                {
                    if (!condicaoDeParada)
                    {
                        Console.WriteLine("\nEscolha uma das opções disponiveis!!");
                        Console.WriteLine("Pressione enter para escolher novamente!");
                        Console.ReadKey();
                        condicaoDeParada = true;
                    }
                }

                switch (opcao)
                {
                    case 1:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Companhias");
                            Console.WriteLine("9 - Voltar ao menu anterior");
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

                            if (opcao < 0 || opcao > 3 && opcao != 9)
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
                                    companhia.AcessarCompanhia(conn, cmd);
                                    break;
                            }

                        } while (opcao != 9);
                        break;

                    case 2:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Aeronaves");
                            Console.WriteLine("9 - Voltar ao menu anterior");
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

                            if (opcao < 0 || opcao > 3 && opcao != 9)
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
                                    aeronave.AcessarAeronave(conn, cmd);
                                    break;
                            }
                        } while (opcao != 9);

                        break;
                 

                    case 3:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Voo");
                            Console.WriteLine("9 - Voltar ao menu anterior");
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

                            if (opcao < 0 || opcao > 3 && opcao != 9)
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
                                    voo.AcessarVoo(conn, cmd);
                                    break;
                            } while (opcao != 9);
                            
                        }while(opcao != 9);
                        break;

                    case 4:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Passagens");
                            Console.WriteLine("9 - Voltar ao menu anterior");
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

                            if (opcao < 0 || opcao > 3 && opcao != 9)
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
                                    passagem.AcessarPassagem(conn,cmd);
                                    break;
                            }

                        } while (opcao != 9);
                        break;

                    case 5:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Vendas");
                            Console.WriteLine("9 - Voltar ao menu anterior");
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

                            if (opcao < 0 || opcao > 3 && opcao != 9)
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
                                    venda.AcessarVenda(conn,cmd);
                                    break;
                            }

                        } while (opcao != 9);
                        break;

                    case 6:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Passageiros");
                            Console.WriteLine("9 - Voltar ao menu anterior");
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

                            if (opcao < 0 || opcao > 3 && opcao != 9)
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
                                    passageiro.AcessarPassageiro(conn,cmd);
                                    break;
                            }

                        } while (opcao != 9);
                        break;

                    case 7:

                        //Console.Clear();

                        //do
                        //{
                        //    Console.WriteLine("                                        <<<<Bem-Vindo(a) a lista de bloqueados e restritos!>>>>                   ");
                        //    Console.WriteLine("\nQual das listas deseja acessar: \n1-CPFs restritos.\n2-CNPJs restritos.\n0-SAIR!\nOpções: ");
                        //    opcao = int.Parse(Console.ReadLine());


                        //    if (opcao == 1)
                        //    {
                        //        do
                        //        {
                        //            Console.WriteLine("                                     <<<<<Bem-vindo(a) ao menu de CPFs restritos:>>>>>                        ");
                        //            Console.WriteLine("\nQual destas ações deseja fazer? : \n1-Imprimir lista de restritos.\n2-Localizar um CPF.\n3-Remover CPF.\n4-Cadastrar um CPF.\n0-SAIR!\nOpção: ");
                        //            opcao = int.Parse(Console.ReadLine());

                        //            if (opcao == 1)
                        //            {
                        //                minhalista.Print();

                        //            }
                        //            if (opcao == 2)
                        //            {
                        //                Console.Clear();

                        //                Console.Write("\nInforme o CPF que deseja localizar: ");
                        //                CPF = Console.ReadLine();
                        //                minhalista.Find(CPF);

                        //            }
                        //            if (opcao == 3)
                        //            {
                        //                Console.Write("\nInforme o CPF que deseja remover: ");
                        //                string cpfremovido = Console.ReadLine();
                        //                minhalista.pop(cpfremovido);

                        //                Console.Clear();

                        //                minhalista.Print();
                        //            }
                        //            if (opcao == 4)
                        //            {

                        //                CPF = ArquivoRestritos.ReadCPF("Informe o cpf sem traço ou ponto: : ");

                        //                minhalista.Push(new ArquivoRestritos(CPF));
                        //                opcao = -1;

                        //                Console.WriteLine("CPF cadastrado com sucesso!");

                        //            }
                        //            else if (opcao == 0)
                        //            {
                        //                Console.Clear();
                        //                Console.WriteLine("Finalizando...");
                        //                return;
                        //            }
                        //            else
                        //                Console.WriteLine("Opção inexistente!");

                        //        } while (true);


                        //    }
                        //    if (opcao == 2)
                        //    {
                        //        do
                        //        {
                        //            Console.WriteLine("                                     <<<<<Bem-vindo(a) ao menu de CNPJs restritos:>>>>>                        ");
                        //            Console.WriteLine("\nQual destas ações deseja fazer? : \n1-Imprimir lista de restritos.\n2-Localizar um CNPJ.\n3-Remover CNPJ.\n4-Cadastrar um CNPJ.\n0-SAIR!\nOpção: ");
                        //            opcao = int.Parse(Console.ReadLine());
                        //        } while (opcao < 1 || opcao > 4);

                        //        if (opcao == 1)
                        //        {
                        //            minhalista1.Print();

                        //        }
                        //        if (opcao == 2)
                        //        {
                        //            Console.Clear();

                        //            Console.Write("\nInforme o CNPJ que deseja localizar: ");
                        //            CNPJ = Console.ReadLine();
                        //            minhalista1.Find(CNPJ);

                        //        }
                        //        if (opcao == 3)
                        //        {
                        //            Console.Write("\nInforme o CNPJ que deseja remover: ");
                        //            string cnpjremovido = Console.ReadLine();
                        //            minhalista1.pop(cnpjremovido);

                        //            Console.Clear();


                        //        }
                        //        if (opcao == 4)
                        //        {
                        //            do
                        //            {
                        //                Console.WriteLine("Informe o CNPJ para o cadastro (XX. XXX. XXX/0001-XX): ");
                        //                CNPJ = Console.ReadLine();

                        //                if (!arquivodeBloqueados.ValidarCnpj(CNPJ))
                        //                {
                        //                    Console.WriteLine("CPNJ digitado é invalido!");
                        //                }

                        //            } while (!arquivodeBloqueados.ValidarCnpj(CNPJ));

                        //            minhalista1.Push(new ArquivoBloqueados(CNPJ));
                        //            opcao = -1;

                        //            Console.WriteLine("CNPJ cadastrado com sucesso!");
                        //        }

                        //        else if (opcao == 0)
                        //        {
                        //            Console.Clear();
                        //            Console.WriteLine("Finalizando...");
                        //            return;
                        //        }

                        //        else
                        //            Console.WriteLine("Opção inexistente!");
                        //    }
                        //    else if (opcao == 0)
                        //    {
                        //        Console.Clear();
                        //        Console.WriteLine("Finalizando...");
                        //        return;
                        //    }
                        //    else
                        //        Console.WriteLine("Opção inexistente!");

                        //} while (true);

                        //break;

                    case 0:
                        cmd.Connection = conn.CloseConexao();
                        Console.WriteLine("\nAté mais.");
                        break;
                }
            } while (opcao != 0);
        }

        internal static int ReadInt(string v)
        {
            throw new NotImplementedException();
        }

        internal static string ReadCPF(string v)
        {
            throw new NotImplementedException();
        }

        internal static bool BuscarNoArray(string op, string[] options)
        {
            throw new NotImplementedException();
        }

        internal static string ReadString(string v)
        {
            throw new NotImplementedException();
        }
    }
}
