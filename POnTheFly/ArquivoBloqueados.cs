using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POnTheFly
{
    public class ArquivoBloqueados
    {
        public string CNPJ { get; set; }
        public ArquivoBloqueados Proximo { get; set; }
        public List<ArquivoRestritos> Filhos { get; set; }
        public ArquivoBloqueados(string CNPJ)
        {

            this.CNPJ = CNPJ;

            this.Proximo = null;
        }

        public override string ToString()
        {
            return "\nDados Restritos:\nCNPJ:" + CNPJ;
        }
        public string getData()
        {
            return CNPJ.PadRight(14).Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
        public void GravarArquivoBloqueados(List<ArquivoBloqueados> arquivodeBloqueados)
        {
            Console.WriteLine("Iniciando a Gravação de Dados...");
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Bloqueados.dat");  //Instancia um Objeto StreamWriter (Classe de Manipulação de Arquivos)
                                                                                                                             //sw.WriteLine("Treinamento de C#");  //Escreve uma linha no Arquivo
                                                                                                                             //sw.WriteLine("maria;araraquara;190;contato;"); //Exemplo de escrita - formato da escrita será de acordo com a necessidade do projeto
                foreach (ArquivoBloqueados i in arquivodeBloqueados)
                {
                    sw.WriteLine(i.getData());
                }
                sw.Close();  // Comando para Fechar o Arquivo
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executando o Bloco de Comandos.");
            }
            Console.WriteLine("FIM DA GRAVAÇÃO");
            Console.ReadKey();

        }
        public void CarregarArquivoBloqueados(List<ArquivoBloqueados> arquivodeBloqueados)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\WATZECK\Desktop\PONTHEFLY\POnTheFly\Bloqueados.dat"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //tempo = new DateTime(int.Parse(line.Substring(14, 4)), int.Parse(line.Substring(12, 2)), int.Parse(line.Substring(10, 2)), int.Parse(line.Substring(20, 2)), int.Parse(line.Substring(18, 2)), int.Parse(line.Substring(18, 2)));
                        arquivodeBloqueados.Add(new ArquivoBloqueados
                            (
                            line.Substring(0, 14)
                            ));
                    }

                    Console.WriteLine("\nArquivo carregado com sucesso!");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("\nException: " + e.Message);
                Console.WriteLine(e.StackTrace);
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
    }
}