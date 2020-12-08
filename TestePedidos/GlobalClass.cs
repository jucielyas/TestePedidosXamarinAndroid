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
using TestePedidos.Models;
using TestePedidos.ModelViews;

namespace TestePedidos
{
    public static class GlobalClass
    {
        public static List<Produto> ListaTodosProdutos { get; set; }
        public static List<Promocao> ListaPromocoes { get; set; }
        public static List<Categoria> ListaCategorias { get; internal set; }
        public static List<mvImagem> ImagensProdutos { get; set; }
        public static Pedido PedidoAtual { get; set; }
    }
}