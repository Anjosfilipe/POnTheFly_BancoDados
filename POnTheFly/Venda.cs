using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POnTheFly;
using System.Data.SqlClient;

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
        public Venda(DateTime dataVenda, string passageiro, double valorTotal)
        {

            DataVenda = dataVenda;
            Passageiro = passageiro;
            ValorTotal = valorTotal;
        }
        public Venda CadastrarVenda(BancoDados conn, SqlCommand cmd)
        {
            PassagemVoo pass = new();
            PassagemVoo pv = pass.LocalizarPassagem(conn, cmd);
            double valorUnitarioPassagem = double.Parse(pv.Valor);
            int qtdPassagensVenda = 0;
            string cpf;

            Console.Clear();

            Console.WriteLine("Digite o CPF do portador sem pontos ou traço: ");
            cpf = Console.ReadLine();

            do
            {
                Console.Write("Digite a quantidade de passagens que deseja comprar" +" (maximo 4 por venda): ");
                qtdPassagensVenda = int.Parse(Console.ReadLine());
                if (qtdPassagensVenda < 1 || qtdPassagensVenda > 4) Console.WriteLine("Quantidade invalida!");
            } while (qtdPassagensVenda < 1 || qtdPassagensVenda > 4);
            Console.WriteLine("Informe o id voo desejado: ");

            Voo voo = new();
            Voo voo1 = voo.LocalizarVoo(conn, cmd);
            Aeronave aeronave = new();
            Aeronave ar = aeronave.Localizar(conn, cmd);

            int passagemlivre = int.Parse(ar.Capacidade);
            
            if (passagemlivre < qtdPassagensVenda)
            {
                Console.WriteLine("A quantidade de passagem livres é menor do que a quantidade a ser vendida! Venda nao realizada");
                return null;
            }

            do
            {
                cmd = new();
                cmd.Connection = conn.OpenConexao();
                cmd.CommandText = "UPDATE  PassagemVoo SET Situacao = 'P',DataUltima_Operacao = @DataUltima_Operacao WHERE Situacao = 'L' AND ID_Voo = @ID_Voo1 AND ID_PassagemVoo = @idpassagem";

                cmd.Parameters.Add(new SqlParameter("@DataUltima_Operacao", DateTime.Now.ToShortDateString()));
                cmd.Parameters.Add(new SqlParameter("@ID_Voo1", voo1.IDVoo));
                cmd.Parameters.Add(new SqlParameter("@idpassagem", passagemlivre));
                cmd.ExecuteNonQuery();
                qtdPassagensVenda--;
                passagemlivre--;
            }while (qtdPassagensVenda != 0);



            Venda venda = new Venda
            (
                DateTime.Now,
                cpf,
                qtdPassagensVenda * valorUnitarioPassagem
            );



            return venda;

        }
        public static void ImprimirVendas(BancoDados conn, SqlCommand cmd)
        {
            Console.Clear();

            Venda vz = new();
            vz.LocalizarVenda(conn, cmd);

        }
        public Venda LocalizarVenda(BancoDados conn, SqlCommand cmd)
        {
            int contador = 0;

            Venda p = new();

            Console.Clear();

            Console.Write("Informe o CPF do Consumidor: ");
            string cpf = Console.ReadLine();

            Console.Write("Informe o id da venda: ");
            string idvenda = Console.ReadLine();

            cmd = new();
            cmd.Connection = conn.OpenConexao();

            cmd.CommandText = "SELECT * FROM Venda WHERE CPF = @CPF AND ID_Venda = @Idvenda";
            cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
            cmd.Parameters.Add(new SqlParameter("@Idvenda", idvenda));
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
                Console.WriteLine("\nPassagem informada não está cadastrada em nosso banco de dados!");
                Console.WriteLine("Pressione enter apra continuar!");

                return null;
            }



            cmd.CommandText = "SELECT * FROM Venda WHERE CPF = @CPF1 AND ID_Venda = @Idvenda1";
            cmd.Parameters.Add(new SqlParameter("@CPF1", cpf));
            cmd.Parameters.Add(new SqlParameter("@Idvenda1", idvenda));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.Clear();
                while (reader.Read())
                {
                    Console.WriteLine("Venda");
                    Console.WriteLine("ID_Venda: {0}", reader.GetInt32(0));
                    Console.WriteLine("Data Venda: {0}", reader.GetString(1));
                    Console.WriteLine("CPF:  {0}", reader.GetString(2));
                    Console.WriteLine("Valor total: {0}", reader.GetString(3));


                    p.Id = reader.GetInt32(0);
                    p.Passageiro = reader.GetString(2);
                }
            }
            Console.WriteLine("\nPressione enter para continuar!");
            Console.ReadKey();
            return p;
        }
        public void AcessarVenda(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            Venda venda = new();
            cmd.Connection = conn.OpenConexao();

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
                        Venda venda1 = new();
                        Venda v = venda1.CadastrarVenda(conn, cmd);
                        Console.ReadKey();
                        cmd.Connection = conn.OpenConexao();

                        cmd.CommandText = $"Insert into Venda (DataVenda, CPF,ValorTotal) Values ('{v.DataVenda.ToShortDateString()}', " +
                        $"'{v.Passageiro}', '{v.ValorTotal}');";

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\t\t\t\t>>>>>>>> CADASTRO REALIZADO COM SUCESSO! <<<<<<<<<<<<");
                        Console.ReadKey();
                        break;
                    case 2:
                        venda.LocalizarVenda(conn, cmd);
                        break;

                    case 3:
                        Venda.ImprimirVendas(conn, cmd);
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