using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POnTheFly
{
    public class ListaRestritos
    {
        public ArquivoRestritos HEAD { get; set; }
        public ArquivoRestritos TAIL { get; set; }

        public ListaRestritos()
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
                ArquivoRestritos aux = HEAD;
                Console.WriteLine("Lista de CPFs restritos: ");
                do
                {
                    Console.WriteLine(aux.ToString() + "\n");
                    aux = aux.Proximo;
                } while (aux != null);
                Console.WriteLine("\nFIM DA Lista");
            }
            Console.ReadKey();
        }

        public void Push(ArquivoRestritos aux)
        {


            //INSERÇÃO LISTA VAZIA
            if (Vazia())
            {
                this.HEAD = this.TAIL = aux;
            }
            else
            {

                if (aux.CPF.CompareTo(TAIL.CPF) >= 0)
                {
                    TAIL.Proximo = aux;
                    TAIL = aux;
                }
                else
                {
                    if (aux.CPF.CompareTo(HEAD.CPF) < 0)
                    {
                        aux.Proximo = HEAD;
                        HEAD = aux;
                    }
                    else
                    {
                        ArquivoRestritos aux1, aux2;
                        aux1 = this.HEAD;
                        aux2 = this.HEAD;

                        do
                        {
                            if (aux.CPF.CompareTo(aux1.CPF) >= 0)
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
        public void pop(string CPFRemovido)
        {
            if (Vazia())
                Console.WriteLine("Lista Vazia! Impossivel remover.");
            else
            {
                HEAD = HEAD.Proximo;
                Console.WriteLine("CPF [" + CPFRemovido + "] removido!");
            }
            Console.WriteLine("\nAperte [enter] para continuar.");
            Console.ReadKey();
        }


        //Localizando
        public void Find(string CPF)
        {
            if (Vazia())
                Console.WriteLine("Lista Vazia!");
            else
            {
                ArquivoRestritos auxiliar = HEAD;
                bool achou = false;
                do
                {
                    if (auxiliar.CPF == CPF)
                    {
                        Console.WriteLine("CPF localizado!\n");
                        Console.WriteLine(auxiliar.ToString());
                        Console.WriteLine("\n");
                        achou = true;
                    }
                    auxiliar = auxiliar.Proximo;
                } while (auxiliar != null);
                if (!achou)
                    Console.WriteLine("CPF [" + CPF + "] não existente.\n");
                else
                    Console.WriteLine("\nFim da lista.");

                Console.WriteLine("\nAperte [enter] para continuar.");
                Console.ReadKey();
            }
        }
    }
}