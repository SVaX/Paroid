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

        private void getApps()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://192.168.1.69:7184");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync("/api/Apps").Result;
                if (response.IsSuccessStatusCode)
                {
                    apps = JsonConvert.DeserializeObject<models.Application[]>(response.Content.ReadAsStringAsync().Result).ToList();
                }
            }
            catch
            {

            }
        }
    }
}