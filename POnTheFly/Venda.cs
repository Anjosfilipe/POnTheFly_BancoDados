using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POnTheFly;

namespace Projeto_OnTheFly
{
    internal class Venda
    {
        //Propriedades
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string Passageiro { get; set; }
        public double ValorTotal { get; set; }
        //Metodos
        public Venda() { }
        public Venda(int id, DateTime dataVenda, string passageiro, double valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            Passageiro = passageiro;
            ValorTotal = valorTotal;
        }
        public Venda CadastrarVenda(List<Venda> listaDeVendas, List<ItemVenda> listaDeItemVenda, List<PassagemVoo> listaDePassagemVoo, List<Voo> listaDeVoo)
        {

            double valorUnitarioPassagem = 1000;
            int tamanhoListaDeVendas = listaDeVendas.Count;
            int qtdPassagensVenda = 0;
            string cpf;

            Console.Clear();

            cpf = Program.ReadCPF("Digite o CPF do portador sem pontos ou traço: ");

            do
            {
                qtdPassagensVenda = Program.ReadInt("Digite a quantidade de passagens que deseja comprar" +
                    " (maximo 4 por venda): ");
                if (qtdPassagensVenda < 1 || qtdPassagensVenda > 4) Console.WriteLine("Quantidade invalida!");
            } while (qtdPassagensVenda < 1 || qtdPassagensVenda > 4);
            Console.WriteLine("Informe o id voo desejado: ");

            Voo voo = new();
            //voo = voo.LocalizarVoo(listaDeVoo);
            if (voo == null)
            {
                Console.WriteLine("\nParametro de entrada é inválido!");
                Console.ReadKey();
                return null;
            }

            int qtdPassagemLivre = 0;
            bool b = false;
            string aux = "V" + voo.IDVoo.ToString().PadRight(4);

            foreach (PassagemVoo passagem in listaDePassagemVoo)
            {
                b = aux == passagem.IdVoo;
                if (b && passagem.Situacao == 'L') qtdPassagemLivre++;
            }
            if (qtdPassagemLivre < qtdPassagensVenda)
            {
                Console.WriteLine("A quantidade de passagem livres é menor do que a quantidade a ser vendida! Venda nao realizada");
                return null;
            }
            int contadorAuxiliar = qtdPassagensVenda;
            foreach (PassagemVoo passagem in listaDePassagemVoo)
            {
                if (passagem.Situacao == 'L' && contadorAuxiliar !=0)
                {
                    passagem.Situacao = 'P';
                    contadorAuxiliar--;
                }
            }

            //Trocar o status da passagem para Paga


            Venda venda = new Venda
            (
                tamanhoListaDeVendas + 1,
                DateTime.Today,
                cpf,
                qtdPassagensVenda * valorUnitarioPassagem
            );

            for (int i = 0; i < qtdPassagensVenda; i++)
            {
                ItemVenda itemVenda = new ItemVenda(venda.Id, 5555, valorUnitarioPassagem);
                listaDeItemVenda.Add(itemVenda);
            }
            return venda;

        }
        public static void ImprimirVendas(List<Venda> listaDeVendas)
        {
            string op = "-1";
            string msg = "";
            string[] options = new string[] { "1", "2", "3", "4", "0" };
            int i = 0;

            while (op != "0")
            {
                Console.Clear();
                Console.WriteLine("*********REGISTROS DE VENDA*********");
                Console.WriteLine(listaDeVendas[i]);
                Console.WriteLine("\n>>> Qtde total de registros: {0}    /    Registro atual: {1}", listaDeVendas.Count, i + 1);
                Console.WriteLine("\nInforme a operacao desejada: ");
                Console.WriteLine("1. Ir para o inicio da lista");
                Console.WriteLine("2. Voltar para o registro anterior");
                Console.WriteLine("3. Avancar para o proximo registro");
                Console.WriteLine("4. Ir para o ultimo registro");
                Console.WriteLine("0. Voltar");
                Console.Write("\n{0}{1}{2}", msg == "" ? "" : ">>> ", msg, msg == "" ? "" : "\n");
                op = Program.ReadString("Opcao: ");
                if (!Program.BuscarNoArray(op, options))
                {
                    msg = "Opcao invalida! Digite novamente...";
                    continue;
                }
                switch (op)
                {
                    case "1":
                        i = 0;
                        break;
                    case "2":
                        if (i != 0) i--;
                        break;
                    case "3":
                        if (i < listaDeVendas.Count - 1) i++;
                        break;
                    case "4":
                        i = listaDeVendas.Count - 1;
                        break;
                    case "0":
                        return;
                }
            }
        }
        public static void LocalizarVenda(List<Venda> listaDeVendas)
        {
            string op = "-1";
            string msg = "";
            int inputId;
            string[] options = new string[] { "1", "0" };
            bool b;

            while (op != "0")
            {
                Console.Clear();
                Console.WriteLine("*********LOCALIZAR VENDA*********");
                Console.WriteLine("\nInforme a operacao desejada: ");
                Console.WriteLine("1. Localizar nova venda");
                Console.WriteLine("0. Voltar");
                Console.Write("\n{0}{1}{2}", msg == "" ? "" : ">>> ", msg, msg == "" ? "" : "\n");
                op = Program.ReadString("Opcao: ");
                if (!Program.BuscarNoArray(op, options))
                {
                    msg = "Opcao invalida! Digite novamente...";
                    continue;
                }
                switch (op)
                {
                    case "1":
                        b = false;
                        inputId = Program.ReadInt("Digite o ID da Venda (apenas numeros): V");

                        foreach (Venda venda in listaDeVendas)
                        {
                            if (venda.Id == inputId)
                            {
                                Console.WriteLine("");
                                Console.WriteLine(venda);
                                Program.ReadString("\nDigite qualquer tecla para continuar...");
                                b = true;
                            };
                        }
                        if (!b) Program.ReadString("\nVenda nao encontrada!\nDigite qualquer tecla para continuar...");
                        break;
                    case "0":
                        return;
                }
            }
        }
        public void AcessarVenda(List<Venda> listaDeVendas, List<ItemVenda> listaDeItemVenda, List<PassagemVoo> listaDePassagemVoo, List<Voo> listaDeVoo)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            Venda venda = new();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR VENDAS\n");

                Console.WriteLine("1 - Cadastrar Venda");
                Console.WriteLine("2 - Localizar Venda");
                Console.WriteLine("3 - Imprimir Venda");
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

                if (opcao < 1 || opcao > 3 && opcao != 9)
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
                        listaDeVendas.Add(venda.CadastrarVenda(listaDeVendas, listaDeItemVenda, listaDePassagemVoo, listaDeVoo));
                        Console.ReadKey();
                        break;

                    case 2:
                        if (listaDeVendas.Count != 0) Venda.LocalizarVenda(listaDeVendas);
                        Console.ReadKey();
                        break;

                    case 3:
                        if (listaDeVendas.Count != 0) Venda.ImprimirVendas(listaDeVendas);
                        Console.ReadKey();
                        break;

                    case 9:
                        Console.WriteLine("Até");
                        break;
                }

            } while (opcao != 9);
        }
        public override string ToString()
        {
            string cpfFormat = String.Format("{0}.{1}.{2}-{3}",
                Passageiro.Substring(0, 3), Passageiro.Substring(3, 3), Passageiro.Substring(6, 3), Passageiro.Substring(9, 2));
            return String.Format("Id:\t\t\tV{0:0000}\n" +
                "Data da Venda:\t\t{1}\n" +
                "CPF do Passageiro:\t{2}\n" +
                "Valor Total da Venda:\tR${3:0.00}",
                Id, DataVenda.ToShortDateString(), cpfFormat, ValorTotal);
        }
    }
}