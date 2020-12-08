using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestePedidos.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Itens = new List<PedidoDetalhe>();
        }
        public int idPedido { get; set; }
        public decimal TotalPedido { get { return Itens.Sum(l => l.Total); } private set { } }
        public List<PedidoDetalhe> Itens { get; set; }
    }
}