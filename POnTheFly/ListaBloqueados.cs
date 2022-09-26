using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace POnTheFly
{
    public class ListadeBloqueados
    {
        public ArquivoBloqueados HEAD { get; set; }
        public ArquivoBloqueados TAIL { get; set; }

        public ListadeBloqueados()
        {
            HEAD = TAIL = null;
        }

        private bool Vazia()
        {
            if ((HEAD == null) && (TAIL == null))
                return true;
            else
                return false;
        }

        public void Print()
        {
            if (Vazia())
            {
                Console.WriteLine("LISTA VAZIA!");
            }
            else
            {
                ArquivoBloqueados aux = HEAD;
                Console.WriteLine("Lista de CNPJs restritos: ");
                do
                {
                    Console.WriteLine(aux.ToString() + "\n");
                    aux = aux.Proximo;
                } while (aux != null);
                Console.WriteLine("\nFIM DA Lista");
            }
            Console.ReadKey();
        }

        public void Push(ArquivoBloqueados aux)
        {
            //INSERÇÃO LISTA VAZIA
            if (Vazia())
            {
                this.HEAD = this.TAIL = aux;
            }
            else
            {

                if (aux.CNPJ.CompareTo(TAIL.CNPJ) >= 0)
                {
                    TAIL.Proximo = aux;
                    TAIL = aux;
                }
                else
                {
                    if (aux.CNPJ.CompareTo(HEAD.CNPJ) < 0)
                    {
                        aux.Proximo = HEAD;
                        HEAD = aux;
                    }
                    else
                    {
                        ArquivoBloqueados aux1, aux2;
                        aux1 = this.HEAD;
                        aux2 = this.HEAD;

                        do
                        {
                            if (aux.CNPJ.CompareTo(aux1.CNPJ) >= 0)
                            {
                                aux2 = aux1;
                                aux1 = aux1.Proximo;
                            }
                            else
                            {

                                aux2.Proximo = aux;
                                aux.Proximo = aux1;

                                break;
                            }
                        } while (true);


                    }
                }
            }
        }

        //removendo
        public void pop(string CNPJRemovido)
        {
            if (Vazia())
                Console.WriteLine("Lista Vazia! Impossivel remover.");
            else
            {
                HEAD = HEAD.Proximo;
                Console.WriteLine("CNPJ [" + CNPJRemovido + "] removido!");
            }
            Console.WriteLine("\nAperte [enter] para continuar.");
            Console.ReadKey();
        }


        //Localizando
        public void Find(string CNPJ)
        {
            if (Vazia())
                Console.WriteLine("Lista Vazia!");
            else
            {
                ArquivoBloqueados auxiliar = HEAD;
                bool achou = false;
                do
                {
                    if (auxiliar.CNPJ == CNPJ)
                    {
                        Console.WriteLine("CNPJ localizado!\n");
                        Console.WriteLine(auxiliar.ToString());
                        Console.WriteLine("\n");
                        achou = true;
                    }
                    auxiliar = auxiliar.Proximo;
                } while (auxiliar != null);
                if (!achou)
                    Console.WriteLine("CNPJ [" + CNPJ + "] não existente.\n");
                else
                    Console.WriteLine("\nFim da lista.");

                Console.WriteLine("\nAperte [enter] para continuar.");
                Console.ReadKey();
            }
        }
    }
}