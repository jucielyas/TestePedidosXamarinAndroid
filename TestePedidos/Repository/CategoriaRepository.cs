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
    public class CategoriaRepository
    {
        public async Task<List<Categoria>> ObterCategorias()
        {
            try
            {
                var listaCategorias = await new ConnectionRequest().Get<List<Categoria>>("YNR2rsWe");

                return (List<Categoria>)listaCategorias;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}