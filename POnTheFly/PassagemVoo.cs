using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.SqlClient;

namespace POnTheFly
{
    internal class PassagemVoo
    {
        public string IdPassagem { get; set; }
        public string IdVoo { get; set; }
        public DateTime DataUltimaOperacao { get; set; }
        public string Valor { get; set; }
        public char Situacao { get; set; }
        public PassagemVoo()
        {
        }
        public PassagemVoo(string idPassagem, string idVoo, DateTime dataUltimaOperacao, string valor, char situacao)
        {
            IdPassagem = idPassagem;
            IdVoo = idVoo;
            DataUltimaOperacao = dataUltimaOperacao;
            Valor = valor;
            Situacao = situacao;
        }

        public void CadastrarPassagem(BancoDados conn, SqlCommand cmd)
        {
            Aeronave ar = new();
            Voo voo = new();

            bool validacao = false;
            double valor;
            int idPassagem = 1;
            

            Console.Clear();

            Aeronave aeronave = ar.Localizar(conn,cmd);
            Voo voo2 = voo.LocalizarVoo(conn,cmd);
         
            do
            {
                Console.Write("Digite o valor das passagens deste voo: R$ ");
                valor = double.Parse(Console.ReadLine());
                validacao = false;

                if (valor > 10000 || valor < 0)
                {
                    Console.WriteLine("\nValor de Passagem fora do limite!\n");
                    validacao = true;
                }

            } while (validacao);

            string stringIdVoo = "" + voo2.IDVoo;
            string stringIdPassagem = "PA" + idPassagem;
            string stringValor = "" + valor;

            for (int i = 0; i < int.Parse(aeronave.Capacidade); i++)
            {
                stringIdPassagem = "" + idPassagem++;

                cmd.CommandText = $"Insert into PassagemVoo (ID_PassagemVoo, ID_Voo, DataUltima_Operacao, Valor, Situacao ) Values ('{stringIdPassagem}', " +
                        $"'{stringIdVoo}', '{DateTime.Now.ToShortDateString()}', '{stringValor}', '{'l'}');";

                cmd.ExecuteNonQuery();
                
              
            }
                Console.WriteLine("\nCadastro de passagens com sucesso!");
        }
        public void EditarPassagem(BancoDados conn, SqlCommand cmd)
        {
            PassagemVoo p = new();
           
            Console.Clear();

            PassagemVoo p1 =  p.LocalizarPassagem(conn,cmd);

            Console.WriteLine("Informe qual dado deseja alterar: ");
            Console.WriteLine("\n1 - Valor");
            Console.WriteLine("2 - Situação");
            Console.WriteLine("0 - Sair");
            Console.Write("\nOpção: ");
            int op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("\nInforme o valor da passagem: R$ ");
                    double valor = double.Parse(Console.ReadLine());

                    if (valor > 9999.99 || valor < 0)
                    {
                        Console.WriteLine("\nValor de Passagem fora do limite!");
                        break;
                    }

                    else
                    {
                        p.Valor = "" + valor;

                        cmd.CommandText = "UPDATE  PassagemVoo SET Valor = @valor WHERE ID_PassagemVoo = @ID_PassagemVoo AND ID_Voo = @ID_Voo";

                        cmd.Parameters.Add(new SqlParameter("@ID_PassagemVoo", p1.IdPassagem));
                        cmd.Parameters.Add(new SqlParameter("@ID_Voo",p1.IdVoo ));
                        cmd.Parameters.Add(new SqlParameter("@valor", valor));

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\n >> Passagem editada com sucesso <<");
                    }
                    break;

                case 2:
                    Console.WriteLine("\nInforme a Situação: ");
                    char situacao = char.Parse(Console.ReadLine());
                    p.Situacao = situacao;

                    cmd.CommandText = "UPDATE  PassagemVoo SET Situacao = @situacao WHERE ID_PassagemVoo = @ID_PassagemVoo1 AND ID_Voo = @ID_Voo1";

                    cmd.Parameters.Add(new SqlParameter("@ID_PassagemVoo1", p1.IdPassagem));
                    cmd.Parameters.Add(new SqlParameter("@ID_Voo1", p1.IdVoo));
                    cmd.Parameters.Add(new SqlParameter("@situacao", p.Situacao));

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("\n >> Passagem editada com sucesso <<");
                    break;
            }
        }/// funcionando
        public PassagemVoo LocalizarPassagem(BancoDados conn, SqlCommand cmd)
        {
            int contador = 0;

            PassagemVoo p = new();

            Console.Clear();

            Console.Write("Informe o id do voo: ");
            int idVoo = int.Parse(Console.ReadLine());

            Console.Write("Informe o id da passagem: ");
            string idPassagem = Console.ReadLine();

            cmd = new();
            cmd.Connection = conn.OpenConexao();

            cmd.CommandText = "SELECT * FROM PassagemVoo WHERE ID_PassagemVoo = @ID_PassagemVoo";
            cmd.Parameters.Add(new SqlParameter("@ID_PassagemVoo", idPassagem));
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

            cmd.CommandText = "SELECT * FROM PassagemVoo WHERE ID_PassagemVoo = @ID_PassagemVoo1 AND ID_Voo = @Idvoo ";
            cmd.Parameters.Add(new SqlParameter("@ID_PassagemVoo1", idPassagem));
            cmd.Parameters.Add(new SqlParameter("@Idvoo", idVoo));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.Clear();
                while (reader.Read())
                {
                    Console.WriteLine("Passagem Voo");
                    Console.WriteLine("ID_PassagemVoo: PA{0}", reader.GetString(0));
                    Console.WriteLine("ID_Voo: V{0}", reader.GetString(1));
                    Console.WriteLine("DataUltima_Operacao:  {0}", reader.GetString(2));
                    Console.WriteLine("Valor: {0}", reader.GetString(3));
                    Console.WriteLine("Situacao: {0}", reader.GetString(4));

                    p.IdPassagem = reader.GetString(0);
                    p.IdVoo = reader.GetString(1);
                    p.Valor = reader.GetString(3);
                }
            }
            Console.WriteLine("\nPressione enter para continuar!");
            Console.ReadKey();
            return p;
        }/// Rodando perfeitamente.
        public void ImprimirPassagem(BancoDados conn, SqlCommand cmd)
        {
            Console.Clear();

            PassagemVoo pvoo = new();
            pvoo.LocalizarPassagem(conn, cmd);
        }
        public void AcessarPassagem(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            PassagemVoo passagem = new();
            cmd.Connection = conn.OpenConexao();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR PASSAGEM\n");

                Console.WriteLine("1 - Cadastrar Passagem");
                Console.WriteLine("2 - Editar Passagem");
                Console.WriteLine("3 - Localizar Passagem");
                Console.WriteLine("4 - Imprimir Passagens");
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
                        passagem.CadastrarPassagem(conn,cmd);
                        Console.ReadKey();
                        break;

                    case 2:
                        passagem.EditarPassagem(conn,cmd);
                        Console.ReadKey();
                        break;

                    case 3:
                        passagem.LocalizarPassagem(conn,cmd);
                        break;

                    case 4:
                        passagem.ImprimirPassagem(conn, cmd);
                        Console.ReadKey();
                        break;
                }

            } while (opcao != 9);
        }
        public string getData()
        {
            return $"{"PA" + IdPassagem.PadRight(4)}{"V" + IdVoo.PadRight(4)}{DataUltimaOperacao.ToString("ddMMyyyyHHmm")}{Valor.PadRight(4)}{Situacao}";
        }
        public override string ToString()
        {
            return "\nID da passagem: " + this.IdPassagem + "\nID do Voo: " + this.IdVoo + "\nData da Ultima Operação: " + this.DataUltimaOperacao + "\nValor da Passagem: R$ " + int.Parse(Valor) + "\nSituação da passagem: " + this.Situacao;

        }
    }
}






