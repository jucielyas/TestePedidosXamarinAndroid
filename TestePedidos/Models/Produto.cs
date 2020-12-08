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
    public class Produto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string photo { get; set; }
        public decimal price { get; set; }
        public int? category_id { get; set; }
    }
}