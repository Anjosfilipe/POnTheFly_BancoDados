using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public void CadastrarPassageiro(List<Passageiro> listaPassageiros)
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

                foreach (var passageiro in listaPassageiros)
                {
                    if (passageiro.Cpf == cpf)
                    {
                        if (true)
                        {
                            Console.WriteLine("\nCpf já se encontra cadastrado!");
                            validacao = true;
                            return;
                        }
                    }
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

            listaPassageiros.Add(new Passageiro(nome, cpf, nascimento, sexo, DateTime.Now, DateTime.Now, Situacao));
        }
        public void LocalizarPassageiro(List<Passageiro> listaPassageiros)
        {
            string op = "-1";
            string msg = "";
            string inputCpf;
            string[] options = new string[] { "1", "0" };
            bool b;

            while (op != "0")
            {
                Console.Clear();
                Console.WriteLine("Localização de passageiro: ");
                Console.WriteLine("\nInforme a operação desejada: ");
                Console.WriteLine("\n01. Localizar");
                Console.WriteLine("00. Voltar");
                Console.Write("\n{0}{1}{2}", msg == "" ? "" : ">>> ", msg, msg == "" ? "" : "\n");
                op = Program.ReadString("Opcao: ");
                if (!Program.BuscarNoArray(op, options))
                {
                    msg = "\nOpção invalida! \nDigite novamente.";
                    continue;
                }
                switch (op)
                {
                    case "1":
                        b = false;
                        Console.Write("\nInforme o cpf do passageiro: ");
                        inputCpf = Console.ReadLine();

                        foreach (Passageiro passageiro in listaPassageiros)
                        {
                            if (passageiro.Cpf == inputCpf)
                            {
                                Console.WriteLine("");
                                Console.WriteLine(passageiro);
                                Program.ReadString("\nVoltando ao menu principal!");
                                b = true;                                                             
                            };
                        }
                        if (!b)
                        {
                            Program.ReadString("\nPassageiro não encontrado!");
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        return;
                }
            }
        }
        public void EditarPassageiro(List<Passageiro> listaPassageiros)
        {
            string cpf;
            bool validacao = true;
            int opcao = 10;
            char sexo;
            Passageiro passageiro = new();

            Console.Clear();

            Console.Write("Informe o cpf do passageiro: ");
            cpf = Console.ReadLine();

            foreach (var p in listaPassageiros)
            {
                if (p.Cpf == cpf)
                {
                    passageiro = p;
                    validacao = true;
                }
            }

            if (!validacao)
            {
                Console.WriteLine("\nCPF do cliente não localizado!");
                return;
            }

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

            } while (opcao != 0);

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

                    Console.WriteLine("\nAlterado com sucesso!");
                }

                else if (opcao == 2)
                {
                    Console.Write("Informe a data de nascimento: ");
                    DateTime nascimento = DateTime.Parse(Console.ReadLine());

                    passageiro.DataNascimento = nascimento;

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
        public void ImprimirPassageiro(List<Passageiro> listaPassageiros)
        {
            Console.Clear();

            if (listaPassageiros.Count == 0)
            {
                Console.WriteLine("\nLista vazia!");
            }
            else
            {
                Console.WriteLine("LISTA DE PASSAGEIROS CADASTRADOS: \n");

                foreach (var passageiro in listaPassageiros)
                {
                    if (passageiro.Situacao == 'A')
                    {
                        Console.WriteLine(passageiro.ToString());
                    }
                }
            }
        }
        public void AcessarPassageiro(List<Passageiro> listaPassageiros)
        {
            int opcao = -1;
            bool condicaoDeParada = false;
            Passageiro passageiro = new();

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
                        passageiro.CadastrarPassageiro(listaPassageiros);
                        Console.ReadKey();
                        break;

                    case 2:
                        passageiro.EditarPassageiro(listaPassageiros);
                        Console.ReadKey();
                        break;

                    case 3:
                        passageiro.LocalizarPassageiro(listaPassageiros);
                        break;

                    case 4:
                        passageiro.ImprimirPassageiro(listaPassageiros);
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