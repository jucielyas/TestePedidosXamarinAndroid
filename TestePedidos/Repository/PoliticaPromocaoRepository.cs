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
    public class PoliticaPromocaoRepository
    {
        public async Task<List<Promocao>> ObterPromocoes()
        {
            try
            {
                var listaPromocoes = await new ConnectionRequest().Get<List<Promocao>>("R9cJFBtG");

                return (List<Promocao>)listaPromocoes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}