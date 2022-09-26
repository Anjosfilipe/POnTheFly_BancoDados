using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Projeto_OnTheFly;

namespace POnTheFly
{
    internal class Program
    {
        #region FuncoesGabriele
        #endregion
        #region FuncoesJulia
        static void GravarArquivoPassageiros(List<Passageiro> passageiros)
        {
            bool validacao = false;

            try
            {


                StreamWriter sw = new StreamWriter(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Passageiros.dat");  //Instancia um Objeto StreamWriter (Classe de Manipulação de Arquivos)


                foreach (var passageiro in passageiros)
                {
                    sw.WriteLine(passageiro.getData());
                }

                sw.Close();  // Comando para Fechar o Arquivo
            }

            catch (Exception)
            {
                Console.WriteLine("\nNão existe companhias para ser gravadas");
                validacao = true;
            }

            if (!validacao)
            {
                Console.WriteLine("\nArquivo gravado com sucesso!");
            }

            return;
        }
        static void CarregarArquivoPassageiros(List<Passageiro> passageiros)
        {
            bool validacao = false;

            try
            {

                using (StreamReader sr = new StreamReader(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Passageiros.dat"))

                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        passageiros.Add(new Passageiro
                            (
                            line.Substring(0, 50),
                            line.Substring(50, 11),
                            new DateTime(int.Parse(line.Substring(65, 4)), int.Parse(line.Substring(63, 2)), int.Parse(line.Substring(61, 2))),
                            char.Parse(line.Substring(69, 1)),
                            new DateTime(int.Parse(line.Substring(74, 4)), int.Parse(line.Substring(72, 2)), int.Parse(line.Substring(70, 2))),
                            new DateTime(int.Parse(line.Substring(82, 4)), int.Parse(line.Substring(80, 2)), int.Parse(line.Substring(78, 2))),
                            char.Parse(line.Substring(86, 1))
                            ));
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                validacao = true;
            }

            if (!validacao)
            {
                Console.WriteLine("\nArquivo carregado com sucesso!");
            }
        }
        #endregion
        #region FuncoesDaniel
        static void ModuloDeCadastros()
        {
            //Inserir aqui todos os cadastros
        }
        static void ModuloDeRegistrosVoo()
        {
            //Inserir aqui a partes do cadastro de voos
        }
        #endregion
        #region ManipulacaoDeArquivosDaniel
        static void CarregarArquivoDeVendas(List<Venda> listaDeVendas)
        {
            try
            {

                using (StreamReader sr = new StreamReader(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Venda.dat"))

                {
                    string line;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        listaDeVendas.Add
                        (
                            new Venda
                            (
                                int.Parse(line.Substring(0, 5)),
                                new DateTime
                                (
                                    int.Parse(line.Substring(9, 4)),
                                    int.Parse(line.Substring(7, 2)),
                                    int.Parse(line.Substring(5, 2))
                                ),
                                line.Substring(13, 11),
                                double.Parse(line.Substring(24, 7)) / 100
                            )
                        );
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        static void GravarArquivoDeVendas(List<Venda> listaDeVendas)
        {
            string dataFormat;
            try
            {

                using (StreamWriter sw = new StreamWriter(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Venda.dat"))

                {
                    foreach (Venda venda in listaDeVendas)
                    {
                        sw.Write("{0:00000}", venda.Id);
                        dataFormat = venda.DataVenda.ToShortDateString();
                        sw.Write("{0}{1}{2}",
                            dataFormat.Substring(0, 2), dataFormat.Substring(3, 2), dataFormat.Substring(6, 4));
                        sw.Write(venda.Passageiro);
                        sw.Write("{0:0000000}\n", venda.ValorTotal * 100);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        static void CarregarArquivoDeItemVenda(List<ItemVenda> listaDeItemVenda)
        {
            try
            {

                using (StreamReader sr = new StreamReader(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\ItemVenda.dat"))

                {
                    string line;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        listaDeItemVenda.Add
                        (
                            new ItemVenda
                            (
                                int.Parse(line.Substring(0, 5)),
                                int.Parse(line.Substring(5, 4)),
                                double.Parse(line.Substring(9, 6)) / 100
                            )
                        );
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        static void GravarArquivoDeItemVendas(List<ItemVenda> listaDeItemVenda)
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\ItemVenda.dat"))

                {
                    foreach (ItemVenda itemVenda in listaDeItemVenda)
                    {
                        sw.Write("{0:00000}", itemVenda.Id);
                        sw.Write("{0:0000}", itemVenda.IdPassagem);
                        sw.Write("{0:000000}\n", itemVenda.ValorUnitario * 100);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        public static string ReadString(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }
        public static int ReadInt(string text)
        {
            Console.Write(text);
            int i;
            while (!int.TryParse(Console.ReadLine(), out i))
                Console.Write("Digite um inteiro valido!\n{0}", text);
            return i;
        }
        public static DateTime ReadDateTime(string text)
        {
            Console.Write(text);
            DateTime d;
            while (!DateTime.TryParse(Console.ReadLine(), out d))
                Console.Write("Digite uma data valida!\n{0}", text);
            return d;
        }
        public static string ReadCPF(string text)
        {
            string cpfString;
            long cpfLong;
            int digVerificador, v1, v2, aux;
            int[] digitosCPF = new int[9];
            bool digitosIguais = false;

            do
            {
                Console.Write(text);
                cpfString = Console.ReadLine();
                while (!long.TryParse(cpfString, out cpfLong))
                {
                    Console.Write("Digite um CPF valido!\n{0}", text);
                    cpfString = Console.ReadLine();
                }
                digVerificador = (int)(cpfLong % 100);
                cpfLong /= 100;
                for (int i = 0; i < 9; i++)
                {
                    aux = (int)cpfLong % 10;
                    digitosCPF[i] = aux;
                    cpfLong /= 10;
                }
                digitosIguais = false;
                for (int i = 0; i < digitosCPF.Length; i++)
                {
                    if (i == digitosCPF.Length - 1)
                    {
                        Console.WriteLine("O CPF nao segue as regras de validacao da Receita Federal!");
                        digitosIguais = true;
                        break;
                    }
                    if (digitosCPF[i] != digitosCPF[i + 1]) break;
                }
                if (digitosIguais) continue;
                v1 = v2 = 0;
                for (int i = 0; i < 9; i++)
                {
                    v1 += digitosCPF[i] * (9 - i);
                    v2 += digitosCPF[i] * (8 - i);
                }
                v1 = (v1 % 11) % 10;
                v2 += v1 * 9;
                v2 = (v2 % 11) % 10;
                if (v1 * 10 + v2 == digVerificador) return cpfString;
                else Console.WriteLine("O CPF nao segue as regras de validacao da Receita Federal!");
            } while (true);
        }
        public static bool BuscarNoArray(string c, string[] list)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i] == c) return true;
            return false;
        }
        #endregion
        #region FuncoesFilipe
        static List<string> IniciarIata()
        {
            List<String> IATA = new List<string>() {"ALC", "AMS", "AJU", "AQP", "AUA", "ASU", "ATH", "ATL", "BWI", "BKK", "BCN", "BRC"
                , "BHZ", "CNF", "PLU", "BER", "TXL", "BIO", "BHM", "BVB", "BOG", "BLK", "BYO", "BOS", "BSB", "BNE", "BRU"
                , "BUE", "CFB", "CLO", "CGR", "CUN", "CCS", "CTG", "CXJ", "XAP", "CLT", "CHI", "MEX", "PTY", "CVG", "CLE"
                , "CGN", "CGH", "CPH", "CPO", "COR", "CGB", "CUR" , "CWB", "CUZ", "BFW", "DNE", "DTW", "DOU", "DXB","DUP"
                , "DUB", "DUS", "EDI", "ARN", "FAO", "FEN" ,"PHL" , "FLR", "FLN", "FLL", "FOR", "IGU", "FRA", "GIG","GVA"
                , "GOA", "GRU", "GYN", "JPR", "JPA", "JOI" , "DEL"};

            return IATA;
        }
        #endregion
        #region FuncoesGiovani
        static void GravarArquivoCompanhias(List<CompanhiaAerea> companhias)
        {
            bool validacao = false;

            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\CompanhiaAerea.dat");  //Instancia um Objeto StreamWriter (Classe de Manipulação de Arquivos)

                foreach (var companhia in companhias)
                {
                    sw.WriteLine(companhia.getData());
                }

                sw.Close();  // Comando para Fechar o Arquivo
            }

            catch (Exception)
            {
                Console.WriteLine("\nNão existe companhias para ser gravadas");
                validacao = true;
            }

            if (!validacao)
            {
                Console.WriteLine("\nArquivo gravado com sucesso!");
            }

            return;
        }
        static void GravarArquivoAeronaves(List<Aeronave> aeronaves)
        {
            bool validacao = false;

            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Aeronave.dat");  //Instancia um Objeto StreamWriter (Classe de Manipulação de Arquivos)

                foreach (var aeronave in aeronaves)
                {
                    sw.WriteLine(aeronave.getData());
                }

                sw.Close();  // Comando para Fechar o Arquivo
            }

            catch (Exception)
            {
                Console.WriteLine("\nNão existe companhias para ser gravadas");
                validacao = true;
            }

            if (!validacao)
            {
                Console.WriteLine("\nArquivo gravado com sucesso!");
            }
        }
        static void CarregarArquivoCompanhias(List<CompanhiaAerea> companhias)
        {
            bool validacao = false;

            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\CompanhiaAerea.dat"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        companhias.Add(new CompanhiaAerea
                            (
                            line.Substring(0, 50),
                            line.Substring(50, 14),
                            new DateTime(int.Parse(line.Substring(68, 4)), int.Parse(line.Substring(66, 2)), int.Parse(line.Substring(64, 2))),
                            new DateTime(int.Parse(line.Substring(76, 4)), int.Parse(line.Substring(74, 2)), int.Parse(line.Substring(72, 2))),
                            new DateTime(int.Parse(line.Substring(84, 4)), int.Parse(line.Substring(82, 2)), int.Parse(line.Substring(80, 2))),
                            char.Parse(line.Substring(88, 1))
                            ));
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
                validacao = true;
            }

            if (!validacao)
            {
                Console.WriteLine("\nArquivo carregado com sucesso!");
            }
        }
        static void CarregarArquivoAeronaves(List<Aeronave> aeronaves)
        {
            bool validacao = false;

            try
            {

                using (StreamReader sr = new StreamReader(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Aeronave.dat"))

                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        aeronaves.Add(new Aeronave
                            (
                            line.Substring(0, 5),
                            line.Substring(5, 3),
                            line.Substring(8, 3),
                            new DateTime(int.Parse(line.Substring(15, 4)), int.Parse(line.Substring(13, 2)), int.Parse(line.Substring(11, 2))),
                            new DateTime(int.Parse(line.Substring(23, 4)), int.Parse(line.Substring(21, 2)), int.Parse(line.Substring(19, 2))),
                            char.Parse(line.Substring(27, 1))
                            ));
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            if (!validacao)
            {
                Console.WriteLine("\nArquivo carregado com sucesso!");
            }
        }
        #endregion

        static void Main(string[] args)
        {
            //Giovani
            List<CompanhiaAerea> listaCompanhias = new();
            CompanhiaAerea companhia = new();
            List<Aeronave> listaAeronaves = new();
            Aeronave aeronave = new();
            //Filipi
            List<Voo> listaVoo = new();
            Voo voo = new();
            List<PassagemVoo> listaPassagens = new();
            PassagemVoo passagem = new();
            List<string> IATA = IniciarIata();
            //Daniel
            List<Venda> listaDeVendas = new();
            Venda venda = new();
            List<ItemVenda> listaDeItemVenda = new();
            ItemVenda itemVenda = new();
            //Julia
            List<Passageiro> listaPassageiros = new();
            Passageiro passageiro = new();
            //Gabriele
            ListaRestritos minhalista = new ListaRestritos();
            minhalista.Push(new ArquivoRestritos("123.321.121-12"));
            ListadeBloqueados minhalista1 = new ListadeBloqueados();
            minhalista1.Push(new ArquivoBloqueados("67.298.128/0001-54"));
            ArquivoBloqueados arquivodeBloqueados = new ArquivoBloqueados(null);
            ArquivoRestritos arquivodeRestritos = new ArquivoRestritos(null);
            List<ListadeBloqueados> listaBloqueados = new List<ListadeBloqueados>();
            List<ListaRestritos> listaRestritos = new List<ListaRestritos>();
            string CPF;
            string CNPJ;
            //Program
            int opcao = 5;
            bool condicaoDeParada;

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
                            Console.WriteLine("2 - Gravar Arquivo Companhias");
                            Console.WriteLine("3 - Carregar Arquivo Companhias");
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
                                    companhia.AcessarCompanhia(listaCompanhias);
                                    break;

                                case 2:
                                    GravarArquivoCompanhias(listaCompanhias);
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    CarregarArquivoCompanhias(listaCompanhias);
                                    Console.ReadKey();
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
                            Console.WriteLine("2 - Gravar Arquivo Aeronaves");
                            Console.WriteLine("3 - Carregar Arquivo Aeronaves");
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
                                    aeronave.AcessarAeronave(listaAeronaves);
                                    break;

                                case 2:
                                    GravarArquivoAeronaves(listaAeronaves);
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    CarregarArquivoAeronaves(listaAeronaves);
                                    Console.ReadKey();
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
                            Console.WriteLine("2 - Gravar Arquivo Voo");
                            Console.WriteLine("3 - Carregar Arquivo Voo");
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
                                    voo.AcessarVoo(listaVoo, listaAeronaves, IATA);
                                    break;

                                case 2:
                                    voo.GravarArquivoVoo(listaVoo);
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    voo.CarregarArquivoVoo(listaVoo);
                                    Console.ReadKey();
                                    break;
                            }

                        } while (opcao != 9);
                        break;

                    case 4:
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Bem-Vindo ao Aeroporto ON THE FLY\n\n");

                            Console.WriteLine("Selecione a opção desejada: ");
                            Console.WriteLine("\n1 - Acessar Passagens");
                            Console.WriteLine("2 - Gravar Arquivo de Passagens");
                            Console.WriteLine("3 - Carregar Arquivo de Passagens");
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
                                    passagem.AcessarPassagem(listaAeronaves, listaVoo, listaPassagens);
                                    break;

                                case 2:
                                    passagem.GravarArquivoPassagem(listaPassagens);
                                    Console.ReadKey();
                                    break;


                                case 3:
                                    passagem.CarregarArquivoPassagem(listaPassagens);
                                    Console.ReadKey();
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
                            Console.WriteLine("2 - Gravar Arquivo de Vendas");
                            Console.WriteLine("3 - Carregar Arquivo de Vendas");
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
                                    venda.AcessarVenda(listaDeVendas, listaDeItemVenda, listaPassagens, listaVoo);
                                    break;

                                case 2:
                                    GravarArquivoDeVendas(listaDeVendas);
                                    GravarArquivoDeItemVendas(listaDeItemVenda);
                                    passagem.GravarArquivoPassagem(listaPassagens);
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    CarregarArquivoDeVendas(listaDeVendas);
                                    CarregarArquivoDeItemVenda(listaDeItemVenda);
                                    passagem.CarregarArquivoPassagem(listaPassagens);
                                    Console.ReadKey();
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
                            Console.WriteLine("2 - Gravar Arquivo de Passageiros");
                            Console.WriteLine("3 - Carregar Arquivo de Passageiros");
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
                                    passageiro.AcessarPassageiro(listaPassageiros);
                                    break;

                                case 2:
                                    GravarArquivoPassageiros(listaPassageiros);
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    CarregarArquivoPassageiros(listaPassageiros);
                                    Console.ReadKey();
                                    break;
                            }

                        } while (opcao != 9);
                        break;

                    case 7:

                        Console.Clear();

                        do
                        {
                            Console.WriteLine("                                        <<<<Bem-Vindo(a) a lista de bloqueados e restritos!>>>>                   ");
                            Console.WriteLine("\nQual das listas deseja acessar: \n1-CPFs restritos.\n2-CNPJs restritos.\n0-SAIR!\nOpções: ");
                            opcao = int.Parse(Console.ReadLine());


                            if (opcao == 1)
                            {
                                do
                                {
                                    Console.WriteLine("                                     <<<<<Bem-vindo(a) ao menu de CPFs restritos:>>>>>                        ");
                                    Console.WriteLine("\nQual destas ações deseja fazer? : \n1-Imprimir lista de restritos.\n2-Localizar um CPF.\n3-Remover CPF.\n4-Cadastrar um CPF.\n0-SAIR!\nOpção: ");
                                    opcao = int.Parse(Console.ReadLine());

                                    if (opcao == 1)
                                    {
                                        minhalista.Print();

                                    }
                                    if (opcao == 2)
                                    {
                                        Console.Clear();

                                        Console.Write("\nInforme o CPF que deseja localizar: ");
                                        CPF = Console.ReadLine();
                                        minhalista.Find(CPF);

                                    }
                                    if (opcao == 3)
                                    {
                                        Console.Write("\nInforme o CPF que deseja remover: ");
                                        string cpfremovido = Console.ReadLine();
                                        minhalista.pop(cpfremovido);

                                        Console.Clear();

                                        minhalista.Print();
                                    }
                                    if (opcao == 4)
                                    {

                                        CPF = ArquivoRestritos.ReadCPF("Informe o cpf sem traço ou ponto: : ");

                                        minhalista.Push(new ArquivoRestritos(CPF));
                                        opcao = -1;

                                        Console.WriteLine("CPF cadastrado com sucesso!");

                                    }
                                    else if (opcao == 0)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Finalizando...");
                                        return;
                                    }
                                    else
                                        Console.WriteLine("Opção inexistente!");

                                } while (true);


                            }
                            if (opcao == 2)
                            {
                                do
                                {
                                    Console.WriteLine("                                     <<<<<Bem-vindo(a) ao menu de CNPJs restritos:>>>>>                        ");
                                    Console.WriteLine("\nQual destas ações deseja fazer? : \n1-Imprimir lista de restritos.\n2-Localizar um CNPJ.\n3-Remover CNPJ.\n4-Cadastrar um CNPJ.\n0-SAIR!\nOpção: ");
                                    opcao = int.Parse(Console.ReadLine());
                                } while (opcao < 1 || opcao > 4);

                                if (opcao == 1)
                                {
                                    minhalista1.Print();

                                }
                                if (opcao == 2)
                                {
                                    Console.Clear();

                                    Console.Write("\nInforme o CNPJ que deseja localizar: ");
                                    CNPJ = Console.ReadLine();
                                    minhalista1.Find(CNPJ);

                                }
                                if (opcao == 3)
                                {
                                    Console.Write("\nInforme o CNPJ que deseja remover: ");
                                    string cnpjremovido = Console.ReadLine();
                                    minhalista1.pop(cnpjremovido);

                                    Console.Clear();


                                }
                                if (opcao == 4)
                                {
                                    do
                                    {
                                        Console.WriteLine("Informe o CNPJ para o cadastro (XX. XXX. XXX/0001-XX): ");
                                        CNPJ = Console.ReadLine();

                                        if (!arquivodeBloqueados.ValidarCnpj(CNPJ))
                                        {
                                            Console.WriteLine("CPNJ digitado é invalido!");
                                        }

                                    } while (!arquivodeBloqueados.ValidarCnpj(CNPJ));

                                    minhalista1.Push(new ArquivoBloqueados(CNPJ));
                                    opcao = -1;

                                    Console.WriteLine("CNPJ cadastrado com sucesso!");
                                }

                                else if (opcao == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Finalizando...");
                                    return;
                                }

                                else
                                    Console.WriteLine("Opção inexistente!");
                            }
                            else if (opcao == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Finalizando...");
                                return;
                            }
                            else
                                Console.WriteLine("Opção inexistente!");

                        } while (true);

                        break;

                    case 0:
                        Console.WriteLine("\nAté mais.");
                        break;
                }
            } while (opcao != 0);
        }
    }
}
