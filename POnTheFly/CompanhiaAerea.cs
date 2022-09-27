using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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

        public CompanhiaAerea CadastrarCompanhia()
        {
            bool condicaoDeSaida = false;
            string numeroCnpj = "1";
            DateTime dataAbertura = new DateTime();


            Console.Clear();

            Console.WriteLine("Vamos iniciar seu cadastro\n");
            Console.Write("Informe a Razão social: ");
            string razaoSocial = Console.ReadLine().ToUpper();


            Console.Write("Informe o número do CNPJ: ");

            numeroCnpj = Console.ReadLine();
            condicaoDeSaida = false;

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
        public CompanhiaAerea LocalizarCompanhia(BancoDados conn, SqlCommand cmd)
        {

            CompanhiaAerea comp = new();
            int contador = 0;
            Console.Clear();
            Console.WriteLine("Olá,");
            Console.Write("\nInforme qual o CNPJ da companhia que deseja localizar: ");
            string cnpjl = Console.ReadLine();

            cmd = new();
            cmd.Connection = conn.OpenConexao();

            cmd.CommandText = "SELECT * FROM  Compania_Aerea WHERE CNPJ = @CNPJ0";
            cmd.Parameters.Add(new SqlParameter("@CNPJ0", cnpjl));
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

            cmd.CommandText = "SELECT * FROM  Compania_Aerea WHERE CNPJ = @CNPJ";
            cmd.Parameters.Add(new SqlParameter("@CNPJ", cnpjl));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.Clear();
                while (reader.Read())
                {
                    Console.WriteLine("COMPANHIA");
                    Console.WriteLine("Nome: {0}", reader.GetString(0));
                    Console.WriteLine("CNPJ: {0}", reader.GetString(1));
                    Console.WriteLine("Data Abertura:  {0}", reader.GetString(2));
                    Console.WriteLine("Último Voo: {0}", reader.GetString(3));
                    Console.WriteLine("Data Cadastro: {0}", reader.GetString(4));
                    Console.WriteLine("Situação: {0}", reader.GetString(5));
                    comp.Cnpj = (reader.GetString(1));
                }
            }


            Console.WriteLine("\nPressione enter para continuar!");
            Console.ReadKey();
            return comp;

        }
        public void EditarCompanhia(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            DateTime dataAbertura = new DateTime();
            string razaoSocial;

            CompanhiaAerea companhia = new CompanhiaAerea();

            CompanhiaAerea comp = companhia.LocalizarCompanhia(conn, cmd);


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

                cmd.CommandText = "UPDATE  Compania_Aerea SET RazaoSocial = @razaoSocial WHERE CNPJ = @CNPJ1";

                cmd.Parameters.Add(new SqlParameter("@CNPJ1", comp.Cnpj));
                cmd.Parameters.Add(new SqlParameter("@razaosocial", companhia.RazaoSocial));

                cmd.ExecuteNonQuery();
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

                    cmd.CommandText = "UPDATE  Compania_Aerea SET DataAbertura = @DataAbertura WHERE CNPJ = @CNPJ2";

                    cmd.Parameters.Add(new SqlParameter("@CNPJ2", comp.Cnpj));
                    cmd.Parameters.Add(new SqlParameter("@DataAbertura", companhia.DataAbertura));

                    cmd.ExecuteNonQuery();
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
                            companhia.Situacao = 'I';

                            cmd.CommandText = "UPDATE  Compania_Aerea SET Situacao = @Situacao WHERE CNPJ = @CNPJ3";

                            cmd.Parameters.Add(new SqlParameter("@CNPJ3", comp.Cnpj));
                            cmd.Parameters.Add(new SqlParameter("@Situacao", companhia.Situacao));

                            cmd.ExecuteNonQuery();

                            Console.WriteLine("\nAlterado com sucesso!");
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
                            companhia.Situacao = 'A';

                            cmd.CommandText = "UPDATE  Compania_Aerea SET Situacao = @Situacao WHERE CNPJ = @CNPJ4";

                            cmd.Parameters.Add(new SqlParameter("@CNPJ4", comp.Cnpj));
                            cmd.Parameters.Add(new SqlParameter("@Situacao", companhia.Situacao));

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("\nAlterado com sucesso!");
                        }

                        else
                        {
                            Console.WriteLine("\nAté logo!");
                        }
                    }
                }
            }

        }
        public void ImprimirCompanhia(BancoDados conn, SqlCommand cmd)
        {

            CompanhiaAerea comp = new CompanhiaAerea();
            comp.LocalizarCompanhia(conn, cmd);
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
        public void AcessarCompanhia(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            CompanhiaAerea companhia = new();
            cmd.Connection = conn.OpenConexao();
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
                        CompanhiaAerea comp = companhia.CadastrarCompanhia();
                        cmd.CommandText = "INSERT INTO Compania_Aerea (RazaoSocial, CNPJ, DataAbertura, UltimoVoo, DataCadatro, Situacao) VALUES (@RazaoSocial, @CNPJ, @DataAbertura, @UltimoVoo, @DataCadatro, @Situacao);";

                        SqlParameter razaosocial = new SqlParameter("@RazaoSocial", System.Data.SqlDbType.VarChar, 50);
                        SqlParameter cnpj = new SqlParameter("@CNPJ", System.Data.SqlDbType.VarChar, 11);
                        SqlParameter dataabertura = new SqlParameter("@DataAbertura", System.Data.SqlDbType.VarChar, 10);
                        SqlParameter ultimovoo = new SqlParameter("@UltimoVoo", System.Data.SqlDbType.VarChar, 10);
                        SqlParameter datacadastro = new SqlParameter("@DataCadatro", System.Data.SqlDbType.VarChar, 10);
                        SqlParameter situacao = new SqlParameter("@Situacao", System.Data.SqlDbType.Char, 1);

                        razaosocial.Value = comp.RazaoSocial;
                        cnpj.Value = comp.Cnpj;
                        dataabertura.Value = comp.DataAbertura;
                        ultimovoo.Value = comp.UltimoVoo;
                        datacadastro.Value = comp.UltimoVoo;
                        situacao.Value = comp.Situacao;

                        cmd.Parameters.Add(razaosocial);
                        cmd.Parameters.Add(cnpj);
                        cmd.Parameters.Add(dataabertura);
                        cmd.Parameters.Add(ultimovoo);
                        cmd.Parameters.Add(datacadastro);
                        cmd.Parameters.Add(situacao);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\t\t\t\t>>>>>>>> CADASTRO REALIZADO COM SUCESSO! <<<<<<<<<<<<");
                        Console.ReadKey();

                        break;

                    case 2:
                        companhia.EditarCompanhia(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("\t\t\t\t>>>>>>>> COMPANIA LOCALIZADA COM SUCESSO! <<<<<<<<<<<<");
                        CompanhiaAerea comp1 = companhia.LocalizarCompanhia(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 4:
                        companhia.ImprimirCompanhia(conn, cmd);
                        Console.ReadKey();
                        break;

                    case 9:
                        cmd.Connection = conn.CloseConexao();
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
