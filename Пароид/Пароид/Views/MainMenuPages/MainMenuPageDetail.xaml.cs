using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = Пароид.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPageDetail : ContentPage
    {
        List<models.Application> apps = new List<models.Application>();
        public MainMenuPageDetail()
        {
            InitializeComponent();
            getApps();
        }

        private void appsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void getApps()
        {
            //SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-U9I41QJ\SQLEXPRESS;Initial Catalog=Diplom;Trusted_Connection = True");
            //conn.Open();
            //SqlCommand command = new SqlCommand("Select * from [Application]", conn);
            //using (SqlDataReader reader = command.ExecuteReader())
            //{
            //    if (reader.Read())
            //    {
            //        var smth = reader[0];
            //    }
            //}
            //conn.Close();

            var httpClientHandler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var requestUri = "https://192.168.1.69:7184/api/Apps";

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                var response = await httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //var app = new models.Application();
                    //app = JsonConvert.DeserializeObject<models.Application>(json);
                }
            }
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://192.168.1.69:7184");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //try
            //{
            //    HttpResponseMessage response = client.GetAsync("/api/Apps").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        apps = JsonConvert.DeserializeObject<models.Application[]>(response.Content.ReadAsStringAsync().Result).ToList();
            //    }
            //}
            //catch
            //{

            //}
        }
    }
}