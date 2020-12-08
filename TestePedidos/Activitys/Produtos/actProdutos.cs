using System;
using System.Net;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using TestePedidos.Repository;
using static Android.Support.Design.Widget.AppBarLayout;
using static Android.Widget.TextView;
using System.Linq;
using TestePedidos.Models;
using System.Collections.Generic;
using TestePedidos.Helpers;
using TestePedidos.ExtensionLayout;

namespace TestePedidos.Activitys.Produtos
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class actProdutos : AppCompatActivity
    {
        private actProdutosLayout LayoutProdutos { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.actProdutos);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            //PENDENTE MENU BOTTOM

            //BottomNavigationView navBottom = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            //navBottom.SetForegroundGravity(GravityFlags.Bottom);
            //navBottom.SetMinimumHeight(new HelperLayout(this).RetornaPixel(40));


            CarregaLayout();

        }



        public void CarregaLayout()
        {
            try
            {

                CoordinatorLayout LayoutPai = FindViewById<CoordinatorLayout>(Resource.Id.LayoutPai);

                LayoutProdutos = new actProdutosLayout(this, LayoutPai);

                LayoutProdutos.AcoesBotaoComprar();

                LayoutProdutos.ObterListas();
                TableLayout listViewProdutos = FindViewById<TableLayout>(Resource.Id.listViewProdutos);
                LayoutProdutos.ListarProdutosLayout(listViewProdutos);

            }
            catch (Exception ex)
            {
                //PENDENTE TRATAMENTO DE EXCEÇÕES
                throw;
            }
        }
 
        public override void OnBackPressed()
        {
            base.OnBackPressed();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (GlobalClass.ListaCategorias == null)
                return false;
            
            menu.Add("TODOS");

            GlobalClass.ListaCategorias.ForEach(l => menu.Add(l.name));
            
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            string title = item.TitleFormatted.ToString();
            LayoutProdutos.AcoesCategorias(title, GlobalClass.ListaCategorias);

            return base.OnOptionsItemSelected(item);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

