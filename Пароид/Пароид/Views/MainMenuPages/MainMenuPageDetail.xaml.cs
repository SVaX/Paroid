using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Xamarin.Essentials;
using Пароид.Models;
using System.Collections.ObjectModel;
using Application = Пароид.Models.Application;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPageDetail : ContentPage
    {
        List<Application> apps = new List<Application>();
        public MainMenuPageDetail()
        {
            InitializeComponent();
            welcomeLabel.Text = "Добро пожаловать, " + Preferences.Get("_currentUserName", "default_value");
            getApps();
        }

        private async void appsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var app = e.Item as Application;
            Preferences.Set("selectedAppName", app.Name);
            await Navigation.PushModalAsync(new GameInfoPage());
        }

        private async void getApps()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Application";
            string selectQuery = String.Format("SELECT * FROM {0}", databaseTable);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteReader();
                //check if account exists
                var exists = result.HasRows;
                var i = 0;
                var appList = new Application[2];
                while (result.Read())
                {
                    var app = new Application();
                    app.AppId = int.Parse(result[0].ToString());
                    app.Name = result[1].ToString();
                    app.Picture = Encoding.UTF8.GetBytes(result[2].ToString());
                    app.Description = result[3].ToString();
                    app.File = Encoding.ASCII.GetBytes(result[4].ToString());
                    app.Rating = int.Parse(result[5].ToString());
                    app.Cost = int.Parse(result[6].ToString());
                    apps.Add(app);
                    var randomList = new List<int>() { 3 };
                    appList[i] = app;
                    
                    i++;
                }
                var list = appList.ToList();

                appsList.ItemsSource = appList.ToList();
            }


        }

        private async void Желаемое_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CartPage());
        }
    }
}