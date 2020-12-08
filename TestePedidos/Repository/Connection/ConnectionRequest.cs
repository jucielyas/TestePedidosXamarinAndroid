using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace TestePedidos.Repository.Connection
{
    public class MsgError
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
    public class ConnectionRequest
    {

        private string _URL { get; set; }
        public bool AguardandoResposta { get; set; }
        public bool? RespostaPing { get; private set; }
        private int _TimeOut { get; set; }
        private Context contexto { get; set; }
        private bool TestandoConexao = false;

        public ConnectionRequest(int TimeOut = 10)
        {
            _TimeOut = TimeOut;
        }

        public async Task<object> Post<T>(string URL, object Data, bool returnObj = true) where T : class, new()
        {
            try
            {

                dynamic tipo = new T();
                string baseAdress = $"https://pastebin.com/raw/";
                var url = new Uri(baseAdress + URL);

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(baseAdress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(Data);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(url, contentString);

                return JsonConvert.DeserializeObject<T>(response.ToString());

            }
            catch (Exception ex)
            {
                return ErrorT<MsgError>("Erro ao tentar enviar a solicitação.\r\nMessage: " + ex.Message);
            }

            //try
            //{
            //    string baseAdress = $"https://pastebin.com/raw/";
            //    var url = new Uri(baseAdress + URL);

            //    string json = JsonConvert.SerializeObject(Data);
            //    HttpClient upload = new HttpClient();
            //    upload.Headers.Clear();
            //    upload.Headers.Add("Content-Type", "application/json");
            //    object x = upload.UploadString(url, json).ToString();

            //    if (returnObj)
            //    {
            //        return JsonConvert.DeserializeObject<T>(x.ToString());
            //    }
            //    else
            //    {
            //        return x;
            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    return ErrorT<MsgError>("Erro ao tentar enviar a solicitação.\r\nMessage: " + ex.Message);
            //}
        }

        int count = 0;
        public async Task<object> Get<T>(string URL) where T : class, new()
        {
            dynamic tipo = new T();

            try
            {
                count++;
                tipo = (T)GetSerialize<T>(URL).Result;

                return tipo;

            }
            catch (Exception ex)
            {
                if (count < 5)
                {
                    return await Get<T>(URL);
                }
                else
                {
                    return tipo;
                }
            }
        }
        private async Task<object> GetSerialize<T>(string URL) where T : class, new()
        {
            dynamic tipo = new T();

            try
            {
                string Retorno = await this.GetString<T>(URL);
                if (Retorno.Contains("error:"))
                {
                    tipo = ErrorT<MsgError>(Retorno);
                }
                else
                {
                    byte[] bytes = Encoding.Default.GetBytes(Retorno);
                    object ret = Encoding.UTF8.GetString(bytes);

                    tipo = JsonConvert.DeserializeObject<T>(ret.ToString());
                }
            }
            catch (System.Exception ex)
            {
                tipo = ErrorT<MsgError>(ex.Message);
            }

            return tipo;
        }
        private async Task<string> GetString<T>(string URL, string ToastMessage = "", Context contexto = null) where T : class, new()
        {

            string baseAdress = $"https://pastebin.com/raw/";
            var url = new Uri(baseAdress + URL);

            HttpClient client = new HttpClient();
            try
            {
                client.Timeout = new TimeSpan(0, 0, _TimeOut);
                //client.BaseAddress = url; // new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> X = client.GetAsync(url);

                if (X.Result.IsSuccessStatusCode)
                {
                    return await X.Result.Content.ReadAsStringAsync();
                }
                else
                {
                    return await Task.FromResult<string>("error: (BAD) Solicitação ao servidor falhou.");
                }
            }
            catch (System.Exception ex)
            {
                return await Task.FromResult<string>("error: " + ex.Message);
            }
        }


        private T ErrorT<T>(string Description, int Code = 0) where T : class, new()
        {
            T tipo = new T();
            MsgError erro = new MsgError() { Codigo = Code, Descricao = "error: " + Description };
            tipo = (T)Convert.ChangeType(erro, typeof(MsgError));
            return tipo;
        }



    }
}