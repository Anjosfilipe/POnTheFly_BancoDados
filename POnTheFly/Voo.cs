using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POnTheFly
{
    public class Voo
    {
        public int IDVoo { get; set; }
        public string Destino { get; set; }
        public string InscricaoAeronave { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }



        public Voo()
        {
        }
        public Voo(int iDVoo, string destino, string aeronave, DateTime dataVoo, DateTime dataCadastro, char situacao)
        {
            IDVoo = iDVoo;
            Destino = destino;
            InscricaoAeronave = aeronave;
            DataVoo = dataVoo;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public Voo CadastrarVoo(List<Voo> listaVoo, List<Aeronave> listAeronave, List<string> listIata)
        {
            Voo voo = new Voo();
            int contador = 0;
            DateTime dataVoo = DateTime.Now;
            bool condicaoDeSaida = false;



            if (voo.IDVoo > 9999)
            {
                Console.WriteLine("Numero maximo de voo cadastrados");
                return null;
            }

            else
            {
                Console.Clear();

                contador = listaVoo.Count + 1;
                Console.Write("Informe o destino do voo: ");
                string destino = Console.ReadLine().ToUpper();

                //// ---- validação da IATA do voo ----- //// 

                bool achei = false;
                foreach (string d in listIata)
                {
                    if (destino == d)
                    {
                        achei = true;
                        //Console.WriteLine("\nDestino  Encontrado!\n");                        
                        // ----- continua o cadastro 
                    }
                }
                if (achei == false)
                {
                    Console.WriteLine("\nInformação não localizada. \nPressione enter para voltar ao menu anterior");
                    return null;
                }
                Console.Write("Informe a incrição da aeronave desejada: ");
                string IdAeronave = Console.ReadLine().ToUpper();
                achei = false;
                // ---- validação do ID da aeronave ------ //

                foreach (var aeronave in listAeronave)
                {
                    if (aeronave.Inscricao == IdAeronave)
                    {
                        achei = true;
                        //Console.WriteLine("\nAeronave encontrada!\n");                        
                    }
                }

                if (achei == false)
                {
                    Console.WriteLine("\nInformação não localizada. \nPressione enter para voltar ao menu anterior");
                    return null;
                }

                else
                {
                    // ----- continua cadastro 


                    do
                    {
                        Console.Write("Infome a data e hora do voo: ");

                        try
                        {
                            dataVoo = DateTime.Parse(Console.ReadLine());
                            condicaoDeSaida = false;
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("\nData e hora informada deve seguir o formato apresentado: (dd/mm/aaaa) (hh:mm)\n");
                            condicaoDeSaida = true;
                        }

                        if (dataVoo < DateTime.Now)
                        {
                            if (!condicaoDeSaida)
                            {
                                Console.WriteLine("\nNão é possivel comprar passagem com data e a hora retroativa!\n");
                                condicaoDeSaida = true;
                            }
                        }

                    } while (condicaoDeSaida);

                    Console.WriteLine("\nCadastro Realizado com sucesso!");

                    return new Voo(contador, destino, IdAeronave, dataVoo, DateTime.Now, 'A');
                }
            }
        }  
        public Voo LocalizarVoo(List<Voo> listaVoo)
        {
            int id = 0;
            bool achei = false;
            Voo v = new Voo();


            Console.Clear();

            Console.Write("Informe o id do Voo com 4 digitos numérios: ");
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("\nParametro de entrada é inválido!");
                return null;
            }

            foreach (Voo i in listaVoo)
            {
                if (i.IDVoo == id)
                {
                    achei = true;
                    v = i;
                    Console.WriteLine("\n>>>>>>> VOO LOCALIZADO <<<<<<");
                    Console.WriteLine(v.ToString());
                    return v;

                }
            }
            if (achei)
            {
                Console.WriteLine("Voo não encontrado!");
            }

            return v;
        }
        public void ImprimirVoo(List<Voo> listaVoo)
        {
            Console.Clear();

            Console.WriteLine(">>>>>>>  LISTA DE VOO CADASTRADOS <<<<<<");

            foreach (Voo voo in listaVoo)
            {


                Console.WriteLine(voo.ToString());

            }
        }
        public void EditarVoo(List<Voo> listaVoo)
        {
            Voo v = new Voo();
            int op = 0;

            Console.Clear();

            v = LocalizarVoo(listaVoo);

            Console.WriteLine("Escolha a opção desejada");
            Console.WriteLine("\n1 - Editar Destino");
            Console.WriteLine("2 - Editar Aeronave");
            Console.WriteLine("3 - Editar Data do voo");
            Console.WriteLine("4 - Situação");
            Console.WriteLine("0 - Sair");
            Console.Write("\nOpção: ");
            op = int.Parse(Console.ReadLine());

            if (op == 1)
            {
                Console.WriteLine("\nInforme o id do novo destino:");
                string destino = Console.ReadLine().ToUpper();
                v.Destino = destino;
                Console.WriteLine("\nAlterado com sucesso!");
            }

            else if (op == 2)
            {
                Console.WriteLine("\nInforme a inscrição da nova Aeronave:");
                string aeronave = Console.ReadLine().ToUpper();
                v.InscricaoAeronave = aeronave;
                Console.WriteLine("\nAlterado com sucesso!");
            }

            else if (op == 3)
            {
                Console.WriteLine("\nInforme a nova data do voo:");
                DateTime data = DateTime.Parse(Console.ReadLine());
                v.DataVoo = data;
                Console.WriteLine("\nAlterado com sucesso!");
            }

            else
            {
                if (op == 4)
                {
                    Console.WriteLine("\nInforme o situação:");
                    char situacao = char.Parse(Console.ReadLine().ToUpper());
                    v.Situacao = situacao;
                    Console.WriteLine("\nAlterado com sucesso!");
                }
            }

        }
        public void AcessarVoo(BancoDados conn, SqlCommand cmd)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            Voo voo = new();
            cmd.Connection = conn.OpenConexao();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR VOO\n");

                Console.WriteLine("1 - Cadastrar Voo");
                Console.WriteLine("2 - Editar Voo");
                Console.WriteLine("3 - Localizar Voo");
                Console.WriteLine("4 - Imprimir Voo");
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

                //switch (opcao)
                //{
                //    case 1:
                //        Voo voo1 = new Voo();
                //        voo1 = voo.CadastrarVoo(listaVoo, listaAeronaves, listIata);

                //        if (voo1 == null)
                //        {
                //        }

                //        else
                //        {
                //            listaVoo.Add(voo1);
                //        }
                //        Console.ReadKey();
                //        break;

                //    case 2:
                //        voo.EditarVoo(listaVoo);
                //        Console.ReadKey();
                //        break;

                //    case 3:
                //        voo.LocalizarVoo(listaVoo);
                //        Console.ReadKey();
                //        break;

                //    case 4:
                //        voo.ImprimirVoo(listaVoo);
                //        Console.ReadKey();
                //        break;

                //    case 9:
                //        cmd.Connection = conn.CloseConexao();
                //        Console.WriteLine("Até");
                //        break;
                //}

            } while (opcao != 9);
        }
        public override string ToString()
        {
            return "\nID Voo:V " + this.IDVoo.ToString("D4") + "\nDestino: " + this.Destino + "\nAeronave: " + this.InscricaoAeronave + "\nData do voo: " + this.DataVoo + "\nData de Cadastro: " + this.DataCadastro + "\nSituação: " + this.Situacao + "\n";
        }
    }
}
