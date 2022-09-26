using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_OnTheFly
{
    internal class ItemVenda
    {
        //Propriedades
        public int Id { get; set; }
        public int IdPassagem { get; set; }
        public double ValorUnitario { get; set; }
        //Metodos
        public ItemVenda() { }
        public ItemVenda(int id, int idPassagem, double valorUnitario)
        {
            Id = id;
            IdPassagem = idPassagem;
            ValorUnitario = valorUnitario;
        }
    }
}