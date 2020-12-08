using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestePedidos.ModelViews
{
    public class mvImagem
    {
        public mvImagem(int _idProduto, Bitmap _imagem)
        {
            idProduto = _idProduto;
            imagem = _imagem;
        }
        public int idProduto { get; set; }
        public Bitmap imagem { get; set; }
    }
}