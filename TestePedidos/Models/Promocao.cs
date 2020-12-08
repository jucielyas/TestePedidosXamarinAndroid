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
    public class Promocao
    {
        public string name { get; set; }
        public int category_id { get; set; }
        public List<PoliticaPromocao> policies { get; set; }
    }
}