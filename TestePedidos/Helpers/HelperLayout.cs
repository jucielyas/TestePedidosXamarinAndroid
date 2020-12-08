using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TestePedidos.Helpers
{
    public class HelperLayout
    {
        private Context context { get; set; }
        public Dialog dialog { get; set; }
        public HelperLayout(Context _Context)
        {
            context = _Context;
        }
        public int RetornaPixel(float dp)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, context.Resources.DisplayMetrics);
          // return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, this.Resources.DisplayMetrics);

        }
        public Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            try
            {

                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

                return imageBitmap;


            }
            catch (Exception ex)
            {
                return imageBitmap;
            }
        }

        public void SetDialog(bool show)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(context);
            builder.SetView(Resource.Layout.progress);
            dialog = dialog == null ? builder.Create() : dialog;
            if (show)
                dialog.Show();
            else
                dialog.Dismiss();

        }
    }
}