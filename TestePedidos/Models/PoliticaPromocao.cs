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
    public class PoliticaPromocao
    {
        public int min { get; set; }
        public decimal discount { get; set; }
    }
}