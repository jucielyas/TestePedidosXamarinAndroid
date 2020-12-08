using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestePedidos.Models;
using TestePedidos.Repository.Connection;

namespace TestePedidos.Repository
{
    public class ProdutoRepository
    {
        public async Task<List<Produto>> ObterProdutos()
        {
            try
            {
                var lista = await new ConnectionRequest().Get<List<Produto>>("eVqp7pfX");

                return (List<Produto>)lista;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}