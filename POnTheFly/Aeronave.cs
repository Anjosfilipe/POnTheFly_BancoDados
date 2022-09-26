using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POnTheFly
{
    public class Aeronave
    {
        public string Inscricao { get; set; }
        public string Tipo { get; set; }
        public string Capacidade { get; set; }
        public string AcentosOcupado { get; set; }
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
            AcentosOcupado = acentosOcupado;
            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public void Cadastrar(List<Aeronave> aeronaves)
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
                    Console.Write("\nInforme a incrição da Aeronave sem o código: ");

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

                foreach (var aeronave in aeronaves)
                {
                    if (aeronave.Inscricao == codigoInscricao)
                    {
                        Console.WriteLine("\nJá existe aeronave com está inscrição!\n");
                        condicaoDeParada = true;                        
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

            aeronaves.Add(new Aeronave(codigoInscricao, strCapacidade, acentosOcupados, DateTime.Now, DateTime.Now, 'A'));
        }
        public Aeronave Localizar(List<Aeronave> aeronaves)
        {
            string inscricao;
            bool validarInscricao;
            Aeronave aeronave = new Aeronave();
            CompanhiaAerea companhia = new();

            Console.Clear();
            Console.WriteLine("Olá,\n");

            do
            {
                Console.Clear();
                Console.WriteLine("Olá,\n");

                Console.Write("Informe a incrição da Aeronave com codigo: ");
                inscricao = Console.ReadLine().ToUpper();
                validarInscricao = true;

                foreach (var aero in aeronaves)
                {
                    if (aero.Inscricao == inscricao)
                    {
                        validarInscricao = false;
                        Console.WriteLine("\nAeronave encontrada\n");
                        Console.WriteLine(aero.ToString());
                        Console.ReadKey();
                        aeronave = aero;
                        return aero;
                    }
                }

                if (validarInscricao)
                {
                    Console.WriteLine("\nInscrição não encontrada!\n");
                    Console.ReadKey();
                    return null;
                }

                else
                {
                    return aeronave;
                }

            } while (validarInscricao);
        }
        public void Imprimir(List<Aeronave> aeronaves)
        {
            Console.Clear();

            Console.WriteLine("LISTA DE AERONAVES\n");

            foreach (var aeronave in aeronaves)
            {
                if (aeronave.Situacao == 'A')
                {
                    Console.WriteLine(aeronave.ToString());
                }
            }
        }
        public void Editar(List<Aeronave> aeronaves)
        {
            int capacidade;
            Aeronave aeronave = new Aeronave();

            Console.Clear();

            aeronave = Localizar(aeronaves);

            if (aeronave == null)
            {
                Console.WriteLine("\nNão encontrado Aeronaves Cadastradas!");
            }

            else
            {
                Console.WriteLine("\nInforme qual será alteração: \n");

                Console.Write("1 - Capacidade\n2 - Situação\n\nOpcão: ");
                int resposta = int.Parse(Console.ReadLine());

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
                            Console.WriteLine("\nAlterado com sucesso!");

                            string strCapacidade = "" + capacidade;

                            aeronave.Capacidade = strCapacidade;
                        }

                    } while (capacidade < 0);

                    Console.WriteLine("\nAlterado com sucesso!");
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
                            Console.WriteLine("\nAlterado com sucesso!");
                            aeronave.Situacao = 'I';
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
        }
        public bool Vazia(List<Aeronave> aeronaves)
        {
            if (aeronaves.Count == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        public void AcessarAeronave(List<Aeronave> aeronaves)
        {
            int opcao = -1;
            bool condicaoDeParada = false;
            Aeronave aeronave = new();

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
                        aeronave.Cadastrar(aeronaves);
                        Console.ReadKey();
                        break;

                    case 2:
                        aeronave.Editar(aeronaves);
                        Console.ReadKey();
                        break;

                    case 3:
                        aeronave.Localizar(aeronaves);
                        Console.ReadKey();
                        break;

                    case 4:
                        aeronave.Imprimir(aeronaves);
                        Console.ReadKey();
                        break;
                }

            } while (opcao != 9);
        }
        public string getData()
        {
            return $"{Inscricao}{Capacidade.PadRight(3)}{AcentosOcupado.PadRight(3)}{UltimaVenda.ToString("ddMMyyyy")}{DataCadastro.ToString("ddMMyyyy")}{Situacao}";
        }
        public override string ToString()
        {
            return $"Inscrição: {Inscricao}\nCapacidade: {Capacidade} Passageiros\nAcentos Ocupados: {AcentosOcupado}\nData da ultima venda: {UltimaVenda.ToShortDateString()}\nData do Cadastro: {DataCadastro.ToLongDateString()}\nSituação: {Situacao}\n";
        }
    }
}
