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
using TestePedidos.Helpers;
using TestePedidos.Models;
using TestePedidos.ModelViews;
using static Android.App.ActionBar;
using static Android.Widget.TextView;

namespace TestePedidos.ExtensionLayout
{
    public class actCarrinhoLayout
    {
        public Context _CONTEXT { get; set; }
        private List<PedidoDetalhe> ListaTodosItens { get; set; }
        private HelperLayout helper { get; set; }
        private LinearLayout LayoutPai { get; set; }
        private LinearLayout LayoutListaProdutos { get; set; }

        public actCarrinhoLayout(Context _context, LinearLayout _LayoutPai)
        {
            _CONTEXT = _context;
            LayoutPai = _LayoutPai;
            helper = new HelperLayout(_CONTEXT);
        }

        public void ListarProdutosLayout()
        {
            try
            {

                LayoutListaProdutos = LayoutPai.FindViewById<LinearLayout>(Resource.Id.LayoutListaPai);
                ListaTodosItens = new List<PedidoDetalhe>();
                foreach (var item in GlobalClass.PedidoAtual.Itens)
                {

                    //CRIAÇÃO DA LINHA
                    int marginLinha = helper.RetornaPixel(5);

                    LinearLayout.LayoutParams paramLinha = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(100));
                    paramLinha.SetMargins(marginLinha, marginLinha, marginLinha, marginLinha);
                    FrameLayout Linha = new FrameLayout(_CONTEXT);
                    Linha.SetBackgroundResource(Resource.Drawable.shape_rounded);
                    Linha.LayoutParameters = paramLinha;

                    int paddingLinearLinha = helper.RetornaPixel(2);
                    LinearLayout.LayoutParams paramLinearLinha = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(100));
                    LinearLayout linearLinha = new LinearLayout(_CONTEXT);
                    linearLinha.Orientation = Orientation.Horizontal;
                    linearLinha.LayoutParameters = paramLinearLinha;
                    linearLinha.SetPadding(paddingLinearLinha, paddingLinearLinha, paddingLinearLinha, paddingLinearLinha);


                    //LINEARLAYOUT DA IMAGEM

                    LinearLayout.LayoutParams paramLinearImagem = new LinearLayout.LayoutParams(helper.RetornaPixel(90), helper.RetornaPixel(100));
                    LinearLayout linearImagem = new LinearLayout(_CONTEXT);
                    linearImagem.Orientation = Orientation.Horizontal;
                    linearImagem.LayoutParameters = paramLinearImagem;


                    LinearLayout.LayoutParams paramImagem = new LinearLayout.LayoutParams(helper.RetornaPixel(80), helper.RetornaPixel(80));
                    ImageView imagem = new ImageView(_CONTEXT);

                    mvImagem existeImagem = GlobalClass.ImagensProdutos.FirstOrDefault(l => l.idProduto == item.idProduto);
                    if (existeImagem != null)
                    {
                        imagem.SetImageBitmap(existeImagem.imagem);
                    }
                    else
                    {
                        var imageBitmap = helper.GetImageBitmapFromUrl(item.produto.photo);
                        if (imageBitmap != null)
                        {
                            imagem.SetImageBitmap(imageBitmap);
                            GlobalClass.ImagensProdutos.Add(new mvImagem(item.idProduto, imageBitmap));
                        }
                    }
                    imagem.LayoutParameters = paramImagem;

                    linearImagem.AddView(imagem);
                    linearLinha.AddView(linearImagem);

                    LinearLayout.LayoutParams paramlinearTitulo = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    paramlinearTitulo.SetMargins(5, 0, 0, 0);
                    LinearLayout linearTitulo = new LinearLayout(_CONTEXT);
                    linearTitulo.Orientation = Orientation.Vertical;
                    linearTitulo.LayoutParameters = paramlinearTitulo;

                    LinearLayout.LayoutParams paramNome = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(50));
                    TextView txtNome = new TextView(_CONTEXT);
                    txtNome.SetText(item.produto.name, BufferType.Normal);
                    txtNome.SetTextColor(Color.Black);
                    txtNome.SetTextSize(Android.Util.ComplexUnitType.Pt, 7);
                    txtNome.LayoutParameters = paramNome;

                    linearTitulo.AddView(txtNome);


                    // LINEARLAYOUT DO VALOR, NOME E DESCONTO

                    LinearLayout.LayoutParams paramLinearValor = new LinearLayout.LayoutParams(LayoutParams.MatchParent, helper.RetornaPixel(110));
                    paramLinearValor.SetMargins(5, 0, 0, 0);
                    LinearLayout linearValor = new LinearLayout(_CONTEXT);
                    linearValor.Orientation = Orientation.Horizontal;
                    linearValor.LayoutParameters = paramLinearValor;


                    // LAYOUT QUANTIDADE

                    LinearLayout.LayoutParams paramLinearQtd = new LinearLayout.LayoutParams(helper.RetornaPixel(60), helper.RetornaPixel(30));
                   
                    LinearLayout linearQtd = new LinearLayout(_CONTEXT);
                    linearQtd.Id = 3000 + item.idProduto;
                    linearQtd.Orientation = Orientation.Horizontal;
                    linearQtd.LayoutParameters = paramLinearQtd;
                    //linearQtd.SetX(-60);
                    //linearQtd.Visibility = ViewStates.Invisible;


                    LinearLayout.LayoutParams paramQtd = new LinearLayout.LayoutParams(LayoutParams.WrapContent, helper.RetornaPixel(30));
                    paramQtd.Gravity = GravityFlags.CenterVertical;

                    TextView txtQtd = new TextView(_CONTEXT);
                    txtQtd.Id = 4000 + item.idProduto;
                    txtQtd.SetText(item.Qtd.ToString(), BufferType.Normal);
                    txtQtd.SetTextSize(Android.Util.ComplexUnitType.Pt, 8);
                    txtQtd.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                    txtQtd.SetPadding(0, helper.RetornaPixel(4), 0, 0);
                    txtQtd.LayoutParameters = paramQtd;

                    TextView txtUN = new TextView(_CONTEXT);
                    txtUN.SetText("UN", BufferType.Normal);
                    txtUN.SetTextSize(Android.Util.ComplexUnitType.Pt, 8);
                    txtUN.SetPadding(helper.RetornaPixel(2), helper.RetornaPixel(4), 0, 0);
                    txtUN.TextAlignment = TextAlignment.TextStart;
                    txtUN.LayoutParameters = paramQtd;

                    linearQtd.AddView(txtQtd);
                    linearQtd.AddView(txtUN);



                    // LAYOUT DESCONTO

                    LinearLayout.LayoutParams paramLinearDesconto = new LinearLayout.LayoutParams(helper.RetornaPixel(65), helper.RetornaPixel(28));
                    //paramLinearDesconto.TopMargin = helper.RetornaPixel(5);
                    paramLinearDesconto.Gravity = GravityFlags.Start;
                    LinearLayout linearDesconto = new LinearLayout(_CONTEXT);
                    linearDesconto.Id = 1000 + item.idProduto;
                    linearDesconto.Orientation = Orientation.Horizontal;
                    linearDesconto.SetBackgroundResource(Resource.Drawable.shape_rounded_red);
                    linearDesconto.LayoutParameters = paramLinearDesconto;
                    linearDesconto.Visibility = ViewStates.Invisible;
                    if (item.Desconto > 0)
                        linearDesconto.Visibility = ViewStates.Visible;

                    linearDesconto.SetGravity(GravityFlags.CenterVertical);
                    linearDesconto.SetPadding(0, 0, 0, 0);

                    LinearLayout.LayoutParams paramImagemDesconto = new LinearLayout.LayoutParams(helper.RetornaPixel(17), helper.RetornaPixel(17));
                    ImageView imgDesconto = new ImageView(_CONTEXT);
                    imgDesconto.SetBackgroundResource(Resource.Mipmap.setaPraBaixoWhite);
                    imgDesconto.LayoutParameters = paramImagemDesconto;
                    // imgDesconto.SetBackgroundColor(Color.OrangeRed);

                    LinearLayout.LayoutParams paramTituloDesconto = new LinearLayout.LayoutParams(helper.RetornaPixel(45), helper.RetornaPixel(30));
                    TextView txtTituloDesconto = new TextView(_CONTEXT);
                    txtTituloDesconto.Id = 2000 + item.idProduto;
                    txtTituloDesconto.SetTextColor(Color.White);
                    txtTituloDesconto.Gravity = GravityFlags.CenterVertical | GravityFlags.Start;
                    txtTituloDesconto.TextAlignment = TextAlignment.Center;
                    // txtTituloDesconto.SetBackgroundColor(Color.Black);
                    txtTituloDesconto.SetText(item.Desconto.ToString("N1") + "%", BufferType.Normal);
                    txtTituloDesconto.SetTextSize(Android.Util.ComplexUnitType.Pt, 8);
                    txtTituloDesconto.LayoutParameters = paramTituloDesconto;
                    txtTituloDesconto.SetPadding(0, helper.RetornaPixel(3),0, 0);

                    linearDesconto.AddView(imgDesconto);
                    linearDesconto.AddView(txtTituloDesconto);


                    LinearLayout.LayoutParams paramValor = new LinearLayout.LayoutParams(helper.RetornaPixel(100), helper.RetornaPixel(30));
                    paramValor.LeftMargin = helper.RetornaPixel(20);
                    paramValor.Gravity = GravityFlags.End;
                    TextView txtValor = new TextView(_CONTEXT);
                    txtValor.Id = 5000 + item.idProduto;
                    txtValor.SetText("R$" + item.Total.ToString("N2"), BufferType.Normal);
                    txtValor.SetTextColor(Color.Black);
                    txtValor.TextAlignment = TextAlignment.TextEnd;
                    txtValor.SetTextSize(Android.Util.ComplexUnitType.Pt, 8);
                    txtValor.LayoutParameters = paramValor;
                    txtValor.SetBackgroundColor(Color.ParseColor("#F2F2F2"));
                    txtValor.SetPadding(0,helper.RetornaPixel(4), helper.RetornaPixel(4), 0);

                    linearValor.AddView(linearQtd);
                    linearValor.AddView(linearDesconto);
                    linearValor.AddView(txtValor);


                    linearTitulo.AddView(linearValor);
                    linearLinha.AddView(linearTitulo);


                    Linha.AddView(linearLinha);
                    LayoutListaProdutos.AddView(Linha);

                    ListaTodosItens.Add(item);

                }


                SetarTotais();

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void SetarTotais()
        {

            TextView txtUnidade = LayoutPai.FindViewById<TextView>(Resource.Id.txtUnidade);
            txtUnidade.SetText(ListaTodosItens.Sum(l => l.Qtd).ToString() + " UN", BufferType.Normal);

            TextView txtTotal = LayoutPai.FindViewById<TextView>(Resource.Id.txtTotal);
            txtTotal.SetText("R$ "+ListaTodosItens.Sum(l => l.Total).ToString("N2"), BufferType.Normal);

        }

    }
}