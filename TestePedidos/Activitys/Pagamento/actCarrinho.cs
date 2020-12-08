using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using TestePedidos.ExtensionLayout;

namespace TestePedidos.Activitys.Pagamento
{
    [Activity(Label = "actCarrinho")]
    public class actCarrinho : AppCompatActivity
    {
        private actCarrinhoLayout LayoutCarrinho{ get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.actCarrinho);

            Toolbar mToolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            mToolbar.Click += MToolbar_Click; ;
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.Title = "Carrinho";

            CarregaLayout();

        }

        private void MToolbar_Click(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        public void CarregaLayout()
        {
            try
            {

                Android.Widget.LinearLayout LayoutPai = FindViewById<Android.Widget.LinearLayout>(Resource.Id.LayoutPai);
                LayoutCarrinho = new actCarrinhoLayout(this, LayoutPai);

                LayoutCarrinho.ListarProdutosLayout();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override void OnBackPressed()
        {
            Finish();
           // base.OnBackPressed();

            
        }

    }
}