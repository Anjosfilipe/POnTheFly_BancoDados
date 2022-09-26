using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POnTheFly
{
    public class CompanhiaAerea
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimoVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }        

        public CompanhiaAerea()
        {
        }
        public CompanhiaAerea(string razaoSocial, string cnpj, DateTime dataAbertura, DateTime ultimoVoo, DateTime dataCadastro, char situacao)
        {
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            DataAbertura = dataAbertura;
            UltimoVoo = ultimoVoo;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }
        public CompanhiaAerea CadastrarCompanhia(List<CompanhiaAerea> companhias)
        {
            bool condicaoDeSaida = false;
            string numeroCnpj = "1";
            DateTime dataAbertura = new DateTime();


            Console.Clear();

            Console.WriteLine("Vamos iniciar seu cadastro\n");
            Console.Write("Informe a Razão social: ");
            string razaoSocial = Console.ReadLine().ToUpper();

            do
            {
                Console.Write("Informe o número do CNPJ: ");

                numeroCnpj = Console.ReadLine();
                condicaoDeSaida = false;

                if (!ValidarCnpj(numeroCnpj))
                {
                    Console.WriteLine("\nCNPJ digitado é inválido!\n");
                    condicaoDeSaida = true;
                }
                else
                {
                    if (companhias.Count == 0)
                    {
                    }

                    else
                    {
                        foreach (var companhia in companhias)
                        {
                            if (companhia.Cnpj == numeroCnpj)
                            {
                                Console.WriteLine($"\nHá companhia {companhia.RazaoSocial} - Situação: {companhia.Situacao} já possue este nome!\n");
                                condicaoDeSaida = true;
                            }
                        }
                    }
                }

            } while (condicaoDeSaida);

            do
            {
                Console.Write("Informe a Data da abertura do CNPJ - (dd/mm/aaaa): ");

                try
                {
                    dataAbertura = DateTime.Parse(Console.ReadLine());
                    condicaoDeSaida = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nData informado deve seguir o formato informado: (dd/mm/aa)\n");
                    condicaoDeSaida = true;
                }

                if (dataAbertura > DateTime.Now)
                {
                    Console.WriteLine("\nData não pode ser maior do que hoje!\n");
                    condicaoDeSaida = true;
                }

            } while (condicaoDeSaida);

            Console.WriteLine("\nCadastrada com sucesso!");

            return new CompanhiaAerea(razaoSocial, numeroCnpj, dataAbertura, DateTime.Now, DateTime.Now, 'A');
        }
        public CompanhiaAerea LocalizarCompanhia(List<CompanhiaAerea> companhias)
        {
            string cnpj;
            bool validacao;
            CompanhiaAerea companhia = new();

            if (Vazia(companhias))
            {
                Console.WriteLine("Lista de companhias vazia!");
                return null;
            }

            else
            {
                Console.Clear();

                Console.WriteLine("Olá,");

                Console.Write("\nInforme qual o CNPJ da companhia que deseja localizar: ");
                cnpj = Console.ReadLine();
                validacao = false;

                foreach (var c in companhias)
                {
                    if (c.Cnpj == cnpj)
                    {
                        Console.WriteLine();
                        Console.WriteLine(c.ToString());
                        validacao = true;
                        companhia = c;
                        return c;
                    }
                }

                if (!validacao)
                {
                    Console.WriteLine("\nCNPJ informado não está cadastrado em nosso banco de dados!");
                    Console.WriteLine("Pressione enter apra continuar!");
                    return null;
                }

                else
                {
                    return companhia;
                }
            }
        }
        public void EditarCompanhia(List<CompanhiaAerea> companhias)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            DateTime dataAbertura = new DateTime();
            string razaoSocial;

            CompanhiaAerea companhia = new CompanhiaAerea();

            if (Vazia(companhias))
            {
                Console.WriteLine("\nNão é possivel editar uma lista vázia!");
            }

            else
            {
                companhia = LocalizarCompanhia(companhias);

                if (companhia == null)
                {
                }

                else
                {
                    do
                    {
                        Console.Clear();

                        Console.WriteLine("Informe qual dado deseja editar: \n");
                        Console.Write("1 - Razão social\n2 - Data de abertura do CNPJ\n3 - Situação \n\n");
                        Console.Write("Opção: ");

                        try
                        {
                            opcao = int.Parse(Console.ReadLine());
                            condicaoDeParada = false;
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("\nParametro informado é inválido!");
                            Console.WriteLine("Pressione enter para continuar!");
                            Console.ReadKey();
                            condicaoDeParada = true;
                        }

                        if (opcao < 1 || opcao > 3)
                        {
                            if (!condicaoDeParada)
                            {
                                Console.WriteLine("\nOpção informada é inválida!");
                                Console.WriteLine("Pressione enter para continuar!");
                                Console.ReadKey();
                                condicaoDeParada = true;
                            }
                        }

                    } while (condicaoDeParada);

                    if (opcao == 1)
                    {
                        Console.Write("\nInforme a nova Razão Social: ");
                        razaoSocial = Console.ReadLine();

                        companhia.RazaoSocial = razaoSocial;

                        Console.WriteLine("\nAlterado com sucesso!");
                    }

                    else
                    {
                        if (opcao == 2)
                        {
                            do
                            {
                                Console.Write("\nInforme a nova Data de abertura do CNPJ (dd/mm/aaaa): ");

                                try
                                {
                                    dataAbertura = DateTime.Parse(Console.ReadLine());
                                    condicaoDeParada = false;
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine("\nData informado deve seguir o formato informado: (dd/mm/aa)\n");
                                    condicaoDeParada = true;
                                }

                            } while (condicaoDeParada);

                            companhia.DataAbertura = dataAbertura;

                            Console.WriteLine("\nAlterado com sucesso!");
                        }

                        else
                        {
                            if (companhia.Situacao == 'A')
                            {
                                Console.WriteLine("\nDeseja alterar a situação desta Aeronave para Inativa?");
                                Console.Write("\n1 - Sim\n2 - Não\n\nOpção: ");
                                opcao = int.Parse(Console.ReadLine());

                                if (opcao == 1)
                                {
                                    Console.WriteLine("\nAlterado com sucesso!");
                                    companhia.Situacao = 'I';
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
                                opcao = int.Parse(Console.ReadLine());

                                if (opcao == 1)
                                {
                                    Console.WriteLine("\nAlterado com sucesso!");
                                    companhia.Situacao = 'A';
                                }

                                else
                                {
                                    Console.WriteLine("\nAté logo!");
                                }
                            }
                        }
                    }
                }
            }
        }
        public void ImprimirCompanhia(List<CompanhiaAerea> companhias)
        {

            if (Vazia(companhias))
            {
                Console.WriteLine("\nLista de companhias vazia!");
            }

            else
            {
                Console.Clear();

                Console.WriteLine("Companhias Aerea Cadastradas: \n");



                foreach (var companhia in companhias)
                {
                    if (companhia.Situacao == 'A')
                    {
                        Console.WriteLine(companhia.ToString());
                    }
                }
            }
        }
        public bool Vazia(List<CompanhiaAerea> companhias)
        {
            if (companhias.Count == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        public bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            string digito, tempCnpj;

            //limpa caracteres especiais e deixa em minusculo
            cnpj = cnpj.ToLower().Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            cnpj = cnpj.Replace("+", "").Replace("*", "").Replace(",", "").Replace("?", "");
            cnpj = cnpj.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "");
            cnpj = cnpj.Replace("%", "").Replace("¨", "").Replace("&", "").Replace("(", "");
            cnpj = cnpj.Replace("=", "").Replace("[", "").Replace("]", "").Replace(")", "");
            cnpj = cnpj.Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "");
            cnpj = cnpj.Replace("<", "").Replace(">", "").Replace("ç", "").Replace("Ç", "");

            // Se vazio
            if (cnpj.Length == 0)
                return false;

            //Se o tamanho for < 14 então retorna como falso
            if (cnpj.Length != 14)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cnpj)
            {

                case "00000000000000":

                    return false;

                case "11111111111111":

                    return false;

                case "22222222222222":

                    return false;

                case "33333333333333":

                    return false;

                case "44444444444444":

                    return false;

                case "55555555555555":

                    return false;

                case "66666666666666":

                    return false;

                case "77777777777777":

                    return false;

                case "88888888888888":

                    return false;

                case "99999999999999":

                    return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            //cnpj é gerado a partir de uma função matemática, logo para validar, sempre irá utilizar esse calculo 
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);

        }
        public void AcessarCompanhia(List<CompanhiaAerea> companhias)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            CompanhiaAerea companhia = new();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR COMPANHIAS\n");

                Console.WriteLine("1 - Cadastrar Companhia Aerea");
                Console.WriteLine("2 - Editar Companhia Aerea");
                Console.WriteLine("3 - Localizar Companhia Aerea");
                Console.WriteLine("4 - Imprimir Companhia Aerea");
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
                        companhias.Add(companhia.CadastrarCompanhia(companhias));
                        Console.ReadKey();
                        break;

                    case 2:
                        companhia.EditarCompanhia(companhias);
                        Console.ReadKey();
                        break;

                    case 3:
                        companhia.LocalizarCompanhia(companhias);
                        Console.ReadKey();
                        break;

                    case 4:
                        companhia.ImprimirCompanhia(companhias);
                        Console.ReadKey();
                        break;

                    case 9:
                        Console.WriteLine("Até");
                        break;
                }

            } while (opcao != 9);
        }
        public string getData()
        {
            return $"{RazaoSocial.PadRight(50)}{Cnpj.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty)}{DataAbertura.ToString("ddMMyyyy")}{UltimoVoo.ToString("ddMMyyyy")}{DataCadastro.ToString("ddMMyyyy")}{Situacao}";
        }
        public override string ToString()
        {
            return $"Razão Social: {RazaoSocial}\nCNPJ: {Cnpj}\nData da Abertura: {DataAbertura.ToShortDateString()}\nData último Voo: {UltimoVoo.ToLongDateString()}\nData do Cadastro: {DataCadastro.ToShortDateString()}\nSituação: {Situacao}\n";
        }
    }
}
