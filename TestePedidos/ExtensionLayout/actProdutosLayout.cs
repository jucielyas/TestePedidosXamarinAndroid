using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using TestePedidos.Activitys.Pagamento;
using TestePedidos.Activitys.Produtos;
using TestePedidos.Helpers;
using TestePedidos.Models;
using TestePedidos.ModelViews;
using TestePedidos.Repository;
using static Android.Support.Design.Widget.AppBarLayout;
using static Android.Widget.TextView;

namespace TestePedidos.ExtensionLayout
{
    public class actProdutosLayout 
    {
        public Context _CONTEXT { get; set; }
        private List<PedidoDetalhe> ListaTodosItens { get; set; }
        public HelperLayout helper { get; set; }
        private CoordinatorLayout LayoutPai { get; set; }
        private TableLayout LayoutListaProdutos { get; set; }
        public bool RatingClicado { get; set; }
        public Button BtnComprar { get; set; }
        public LinearLayout LayoutBtnComprar { get; set; }
        public actProdutosLayout(Context _context, CoordinatorLayout _LayoutPai)
        {


            _CONTEXT = _context;
            LayoutPai = _LayoutPai;
            helper = new HelperLayout(_CONTEXT);

            LayoutBtnComprar = (LinearLayout)LayoutPai.FindViewById(Resource.Id.layoutBtnComprar);
            BtnComprar = LayoutBtnComprar.FindViewById<Button>(Resource.Id.btnComprar);
            BtnComprar.Click += BtnComprar_Click;

            GlobalClass.PedidoAtual = GlobalClass.PedidoAtual ?? new Pedido();
        }

        public void ListarProdutosLayout(TableLayout _LayoutListaProdutos, Categoria _categoriaSelecionada = null)
        {
            try
            {
                var dadosLocais = Application.Context.GetSharedPreferences("MeuArquivo", Android.Content.FileCreationMode.Private);

                _LayoutListaProdutos.RemoveAllViews();

                LayoutListaProdutos = _LayoutListaProdutos;

                //GlobalClass.ListaTodosProdutos = GlobalClass.ListaTodosProdutos == null ? await new ProdutoRepository().ObterProdutos() : GlobalClass.ListaTodosProdutos;
                //GlobalClass.ListaPromocoes = GlobalClass.ListaPromocoes == null ? await new PoliticaPromocaoRepository().ObterPromocoes() : GlobalClass.ListaPromocoes;

                GlobalClass.ImagensProdutos = GlobalClass.ImagensProdutos == null ? new List<mvImagem>() : GlobalClass.ImagensProdutos;

                ListaTodosItens = new List<PedidoDetalhe>();

                List<Produto> ListaProdutos = new List<Produto>();
                if (_categoriaSelecionada != null)
                    ListaProdutos = GlobalClass.ListaTodosProdutos.Where(l => l.category_id == _categoriaSelecionada.id).ToList();
                else
                    ListaProdutos = GlobalClass.ListaTodosProdutos;


                foreach (var item in ListaProdutos)
                {
                    PedidoDetalhe ItemPedido = new PedidoDetalhe()
                    {
                        idProduto = item.id,
                        ValorUnitario = item.price,
                        produto = item
                    };
                    

                    //CRIAÇÃO DA LINHA
                    int marginLinha = helper.RetornaPixel(5);

                    TableLayout.LayoutParams paramTable = new TableLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(130));
                    TableLayout table = new TableLayout(_CONTEXT);
                    table.SetPadding(marginLinha, marginLinha, marginLinha, marginLinha);

                    LinearLayout.LayoutParams paramLinha = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(130));
                    FrameLayout Linha = new FrameLayout(_CONTEXT);
                    Linha.SetBackgroundResource(Resource.Drawable.shape_rounded);
                    Linha.LayoutParameters = paramLinha;

                    int paddingLinearLinha = helper.RetornaPixel(1);
                    LinearLayout.LayoutParams paramLinearLinha = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(130));
                    LinearLayout linearLinha = new LinearLayout(_CONTEXT);
                    linearLinha.Orientation = Orientation.Horizontal;
                    linearLinha.LayoutParameters = paramLinearLinha;
                    linearLinha.SetPadding(paddingLinearLinha, paddingLinearLinha, paddingLinearLinha, paddingLinearLinha);


                    //LINEARLAYOUT DA IMAGEM

                    LinearLayout.LayoutParams paramLinearImagem = new LinearLayout.LayoutParams(helper.RetornaPixel(90), helper.RetornaPixel(120));
                    LinearLayout linearImagem = new LinearLayout(_CONTEXT);
                    linearImagem.Orientation = Orientation.Horizontal;
                    linearImagem.LayoutParameters = paramLinearImagem;


                    LinearLayout.LayoutParams paramImagem = new LinearLayout.LayoutParams(helper.RetornaPixel(90), helper.RetornaPixel(90));
                    ImageView imagem = new ImageView(_CONTEXT);

                    mvImagem existeImagem = GlobalClass.ImagensProdutos.FirstOrDefault(l => l.idProduto == item.id);
                    if (existeImagem != null)
                    {
                        imagem.SetImageBitmap(existeImagem.imagem);
                    }
                    else
                    {
                        var imageBitmap = helper.GetImageBitmapFromUrl(ItemPedido.produto.photo);
                        if (imageBitmap != null)
                        {
                            imagem.SetImageBitmap(imageBitmap);
                            GlobalClass.ImagensProdutos.Add(new mvImagem(item.id, imageBitmap));
                        }
                    }
                    imagem.LayoutParameters = paramImagem;

                    linearImagem.AddView(imagem);
                    linearLinha.AddView(linearImagem);


                    // LINEARLAYOUT DO VALOR, NOME E DESCONTO

                    LinearLayout.LayoutParams paramLinearValor = new LinearLayout.LayoutParams(helper.RetornaPixel(130), helper.RetornaPixel(130));
                    paramLinearValor.SetMargins(5, 0, 0, 0);
                    LinearLayout linearValor = new LinearLayout(_CONTEXT);
                    linearValor.Orientation = Orientation.Vertical;
                    linearValor.LayoutParameters = paramLinearValor;

                    LinearLayout.LayoutParams paramNome = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(50));
                    TextView txtNome = new TextView(_CONTEXT);
                    txtNome.SetText(ItemPedido.produto.name, BufferType.Normal);
                    txtNome.SetTextColor(Color.Black);
                    txtNome.SetTextSize(Android.Util.ComplexUnitType.Pt, 7);
                    txtNome.LayoutParameters = paramNome;

                    linearValor.AddView(txtNome);

                    LinearLayout.LayoutParams paramLinearDesconto = new LinearLayout.LayoutParams(helper.RetornaPixel(50), helper.RetornaPixel(20));
                    paramLinearDesconto.TopMargin = helper.RetornaPixel(5);
                    LinearLayout linearDesconto = new LinearLayout(_CONTEXT);
                    linearDesconto.Id = 1000 + item.id;
                    linearDesconto.Orientation = Orientation.Horizontal;
                    linearDesconto.SetBackgroundResource(Resource.Drawable.shape_rounded_red);
                    linearDesconto.LayoutParameters = paramLinearDesconto;
                    linearDesconto.Visibility = ViewStates.Invisible;
                    linearDesconto.SetGravity(GravityFlags.CenterVertical);
                    linearDesconto.SetPadding(0, 0, 0, 0);

                    LinearLayout.LayoutParams paramImagemDesconto = new LinearLayout.LayoutParams(helper.RetornaPixel(16), helper.RetornaPixel(16));
                    ImageView imgDesconto = new ImageView(_CONTEXT);
                    imgDesconto.SetBackgroundResource(Resource.Mipmap.setaPraBaixoWhite);
                    imgDesconto.LayoutParameters = paramImagemDesconto;
                   // imgDesconto.SetBackgroundColor(Color.OrangeRed);

                    LinearLayout.LayoutParams paramTituloDesconto = new LinearLayout.LayoutParams(helper.RetornaPixel(35), helper.RetornaPixel(20));
                    TextView txtTituloDesconto = new TextView(_CONTEXT);
                    txtTituloDesconto.Id = 2000+item.id;
                    txtTituloDesconto.SetTextColor(Color.White);
                    txtTituloDesconto.Gravity = GravityFlags.CenterVertical | GravityFlags.Start;
                    txtTituloDesconto.TextAlignment = TextAlignment.Center;
                   // txtTituloDesconto.SetBackgroundColor(Color.Black);
                    txtTituloDesconto.SetText(ItemPedido.Desconto.ToString("N1") + "%", BufferType.Normal);
                    txtTituloDesconto.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
                    txtTituloDesconto.LayoutParameters = paramTituloDesconto;
                    txtTituloDesconto.SetPadding(0, 0, 0, 0);

                    linearDesconto.AddView(imgDesconto);
                    linearDesconto.AddView(txtTituloDesconto);
                    linearValor.AddView(linearDesconto);

                    LinearLayout.LayoutParams paramValor = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(50));
                    TextView txtValor = new TextView(_CONTEXT);
                    txtValor.Id = 5000 + item.id;
                    txtValor.SetText("R$"+item.price.ToString("N2"), BufferType.Normal);
                    txtValor.SetTextColor(Color.Black);
                    txtValor.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
                    txtValor.LayoutParameters = paramValor;

                    linearValor.AddView(txtValor);
                    linearLinha.AddView(linearValor);


                    //LINEARLAYOUT DO RATINGBAR(FAVORITAR) E QUANTIDADE

                    LinearLayout.LayoutParams paramLinearRating = new LinearLayout.LayoutParams(helper.RetornaPixel(130), LayoutParams.WrapContent);
                    LinearLayout linearRatingBar = new LinearLayout(_CONTEXT);
                    linearRatingBar.LayoutParameters = paramLinearRating;

                    LinearLayout.LayoutParams paramRating = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                    // paramRating.SetMargins(0, 0, 0, 40);
                    RatingBar ratingBar = new RatingBar(_CONTEXT);
                    ratingBar.StepSize = 0.5f;
                    ratingBar.NumStars = 1;
                    ratingBar.Rating = 0;
                    ratingBar.Tag = false;
                    ratingBar.Id = item.id;
                    ratingBar.ScaleX = 0.5f;
                    ratingBar.ScaleY = 0.5f;
                    ratingBar.IsIndicator = false;
                    ratingBar.SetX(140);
                    ratingBar.SetY(-30);
                    ratingBar.LayoutParameters = paramRating;
                    ratingBar.RatingBarChange += Rating_RatingBarChange;


                    /////////   PENDENTE A PERSISTÊNCIA DE FAFORITOS POR SQLITE  /////////
                    var rating = dadosLocais.GetBoolean(ratingBar.Id.ToString(), false);
                    ratingBar.Rating = rating ? 1 : 0;

                    linearRatingBar.AddView(ratingBar);

                    LinearLayout.LayoutParams paramLinearQtd = new LinearLayout.LayoutParams(helper.RetornaPixel(130), helper.RetornaPixel(30));
                    paramLinearQtd.TopMargin = 80;
                    LinearLayout linearQtd = new LinearLayout(_CONTEXT);
                    linearQtd.Id = 3000 + item.id;
                    linearQtd.Orientation = Orientation.Horizontal;
                    linearQtd.LayoutParameters = paramLinearQtd;
                    linearQtd.SetX(-60);
                    linearQtd.Visibility = ViewStates.Invisible;

                    LinearLayout.LayoutParams paramQtd = new LinearLayout.LayoutParams(helper.RetornaPixel(30), helper.RetornaPixel(30));
                    TextView txtQtd = new TextView(_CONTEXT);
                    txtQtd.Id = 4000 + item.id;
                    txtQtd.SetText("1", BufferType.Normal);
                    txtQtd.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
                    txtQtd.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                    txtQtd.Gravity = GravityFlags.End;
                    txtQtd.LayoutParameters = paramQtd;

                    TextView txtUN = new TextView(_CONTEXT);
                    txtUN.SetText("UN", BufferType.Normal);
                    txtUN.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
                    txtUN.Gravity = GravityFlags.Start;
                    txtUN.SetPadding(5, 0, 0, 0);
                    txtUN.LayoutParameters = paramQtd;

                    linearQtd.AddView(txtQtd);
                    linearQtd.AddView(txtUN);

                    linearRatingBar.AddView(linearQtd);


                    //linearRatingBar.AddView(linearQtd);

                    LinearLayout.LayoutParams paramLinearBtnQtd = new LinearLayout.LayoutParams(helper.RetornaPixel(80), helper.RetornaPixel(30));
                    paramLinearBtnQtd.TopMargin = 150;
                    
                    LinearLayout linearBtnQtd = new LinearLayout(_CONTEXT);
                    linearBtnQtd.Orientation = Orientation.Horizontal;
                    linearBtnQtd.SetBackgroundResource(Resource.Drawable.shape_rounded_gray);
                    linearBtnQtd.SetPadding(0, 0, 0, 0);
                    linearBtnQtd.SetGravity(GravityFlags.CenterVertical);
                    linearBtnQtd.LayoutParameters = paramLinearBtnQtd;
                    linearBtnQtd.SetX(-330);
                    //linearBtnQtd.SetY(-50);


                    LinearLayout.LayoutParams paramBtnQtd = new LinearLayout.LayoutParams(helper.RetornaPixel(40), helper.RetornaPixel(30));
                    paramBtnQtd.SetMargins(0, 0, 0, 0);

                    ImageButton btnMenos = new ImageButton(_CONTEXT);
                    btnMenos.SetBackgroundColor(Color.Transparent);
                    btnMenos.SetImageResource(Resource.Drawable.buttonRemove);
                    btnMenos.LayoutParameters = paramBtnQtd;
                    btnMenos.Tag = item.id;
                    btnMenos.Click += BtnMenos_Click;
                    
                    ImageButton btnMais = new ImageButton(_CONTEXT);
                    btnMais.SetBackgroundColor(Color.Transparent);
                    btnMais.SetImageResource(Resource.Drawable.buttonAdd);
                    btnMais.LayoutParameters = paramBtnQtd;
                    btnMais.Tag = item.id;
                    btnMais.Click += BtnMais_Click;

                    linearBtnQtd.AddView(btnMenos);
                    linearBtnQtd.AddView(btnMais);

                    linearRatingBar.AddView(linearBtnQtd);

                    linearLinha.AddView(linearRatingBar);

                    Linha.AddView(linearLinha);
                    table.AddView(Linha);
                    LayoutListaProdutos.AddView(table);

                    ListaTodosItens.Add(ItemPedido);

                }


                foreach (var item in GlobalClass.PedidoAtual.Itens)
                {
                    AtualizarItemNoLayout(item);

                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async void ObterListas()
        {
            try
            {
                GlobalClass.ListaCategorias = await new CategoriaRepository().ObterCategorias();
                GlobalClass.ListaTodosProdutos = GlobalClass.ListaTodosProdutos == null ? await new ProdutoRepository().ObterProdutos() : GlobalClass.ListaTodosProdutos;
                GlobalClass.ListaPromocoes = GlobalClass.ListaPromocoes == null ? await new PoliticaPromocaoRepository().ObterPromocoes() : GlobalClass.ListaPromocoes;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void AcoesCategorias(string nomeCategoria, List<Categoria> _Categorias)
        {
            try
            {
                Categoria categoriaSelecionada = _Categorias.FirstOrDefault(l => l.name.Trim() == nomeCategoria.Trim());
                ListarProdutosLayout(LayoutListaProdutos, categoriaSelecionada);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AcoesBotaoComprar()
        {
            try
            {

                CoordinatorLayout.LayoutParams paramLayout = new CoordinatorLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                paramLayout.TopMargin = helper.RetornaPixel(70);
                LinearLayout LayoutListaPai = (LinearLayout)LayoutPai.FindViewById(Resource.Id.LayoutListaPai);
                LayoutListaPai.LayoutParameters = paramLayout;

                 LayoutBtnComprar = (LinearLayout)LayoutPai.FindViewById(Resource.Id.layoutBtnComprar);
                LayoutBtnComprar.Visibility = ViewStates.Invisible;

                if (GlobalClass.PedidoAtual.Itens.Count > 0)
                {
                    LayoutBtnComprar.Visibility = ViewStates.Visible;

                    string icon = "▶";
                    Button BtnComprar = LayoutBtnComprar.FindViewById<Button>(Resource.Id.btnComprar);
                    BtnComprar.SetText("Comprar" + icon + "R$" + GlobalClass.PedidoAtual.TotalPedido.ToString("N2"), BufferType.Normal);
                    
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtnComprar_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(_CONTEXT, typeof(actCarrinho));
            _CONTEXT.StartActivity(it);
        }

        public void AlterarQuantidade(bool Aumentar, int idProduto)
        {
            try
            {
                PedidoDetalhe ItemAtual = ListaTodosItens.FirstOrDefault(l => l.idProduto == idProduto);

                PedidoDetalhe existeItemNoPedido = GlobalClass.PedidoAtual.Itens.FirstOrDefault(l => l.idProduto == idProduto);
                if (existeItemNoPedido != null)                  
                    ItemAtual = existeItemNoPedido;

                int index = -1;
                if (Aumentar)
                {
                    ItemAtual.Qtd++;

                }
                else
                {
                    index = GlobalClass.PedidoAtual.Itens.FindIndex(l => l.idProduto == idProduto);
                    if (ItemAtual.Qtd <= 1)
                    {
                        GlobalClass.PedidoAtual.Itens.RemoveAt(index);
                    }

                    ItemAtual.Qtd--;
                }

                 index = GlobalClass.PedidoAtual.Itens.FindIndex(l => l.idProduto == idProduto);
                if (index > -1)
                    GlobalClass.PedidoAtual.Itens[index] = ItemAtual;
                else
                {
                    if (Aumentar)
                        GlobalClass.PedidoAtual.Itens.Add(ItemAtual);
                }

                AtualizarItemNoLayout(ItemAtual);
                AcoesBotaoComprar();

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void AtualizarItemNoLayout(PedidoDetalhe ItemAtual)
        {
            
            LinearLayout linearQtd = (LinearLayout)LayoutListaProdutos.FindViewById(3000 + ItemAtual.idProduto);
            TextView txtQtd = (TextView)LayoutListaProdutos.FindViewById(4000 + ItemAtual.idProduto);

            LinearLayout linearDesconto = (LinearLayout)LayoutListaProdutos.FindViewById(1000 + ItemAtual.idProduto);
            TextView txtDesconto = (TextView)LayoutListaProdutos.FindViewById(2000 + ItemAtual.idProduto);

            TextView txtValor = (TextView)LayoutListaProdutos.FindViewById(5000 + ItemAtual.idProduto);

            linearQtd.Visibility = ItemAtual.Qtd > 0 ? ViewStates.Visible : ViewStates.Invisible;
            txtQtd.SetText(ItemAtual.Qtd.ToString(),BufferType.Normal);
            
            linearDesconto.Visibility = ItemAtual.Desconto > 0 ? ViewStates.Visible : ViewStates.Invisible; ;
            txtDesconto.SetText(ItemAtual.Desconto.ToString("N1") + "%", BufferType.Normal);

           // txtValor.SetText("R$" + ItemAtual.ValorUnitario.ToString("N2"), BufferType.Normal);


        }

        private void BtnMais_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            AlterarQuantidade(true, (int)btn.Tag);
        }

        private void BtnMenos_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            AlterarQuantidade(false, (int)btn.Tag);
        }

        private void Rating_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            var dadosLocais = Application.Context.GetSharedPreferences("MeuArquivo", Android.Content.FileCreationMode.Private);

            if (RatingClicado)
                return;
            RatingBar rating = (RatingBar)sender;
            RatingClicado = true;

            rating.Tag = !(bool)rating.Tag;
            rating.Rating = (bool)rating.Tag ? 1 : 0;

            ISharedPreferencesEditor editor = dadosLocais.Edit();
            editor.PutBoolean(rating.Id.ToString(), (bool)rating.Tag);
            editor.Apply();


            RatingClicado = false;

          }



    }
}