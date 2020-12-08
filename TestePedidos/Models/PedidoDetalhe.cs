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
    public class PedidoDetalhe
    {

        public int idPedidoDetalhe { get; set; }
        public int idProduto { get; set; }
        public decimal Desconto { get { return SetarDesconto(); } private set { SetarDesconto(); } }
        public int Qtd { get; set; }
        public decimal ValorUnitario { get; set; }
            
        public decimal Total 
        {
            get 
            {
                if (Desconto > 0)
                {
                    decimal desconto = (Desconto / 100) * ValorUnitario;
                    return (ValorUnitario - desconto) * Qtd;
                }
                else
                {
                    return ValorUnitario * Qtd;
                }
            }
            private set { }
        }
        public Produto produto { get; set; }
        
        private decimal SetarDesconto()
        {
            try
            {
                List<PoliticaPromocao> politicas = new List<PoliticaPromocao>();
                GlobalClass.ListaPromocoes?
                    .Where(l => l.category_id == produto.category_id).ToList()
                    .ForEach(l => politicas.AddRange(l.policies));

                PoliticaPromocao melhorPolitica = null;
                foreach (var item in politicas)
                {
                    if (Qtd >= item.min)
                        melhorPolitica = item;
                }

                if (melhorPolitica == null)
                    return 0;


                return melhorPolitica.discount;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }


}