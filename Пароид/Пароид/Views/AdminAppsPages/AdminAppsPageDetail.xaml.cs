using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Application = Пароид.Models.Application;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminAppsPageDetail : ContentPage
    {
        List<Application> appsLib = new List<Application>();
        public AdminAppsPageDetail()
        {
            InitializeComponent();
            getApps();
        }

        private async void getApps()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Application";
            string selectQuery = $"SELECT * FROM {databaseTable}";
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
                List<Application> apps = new List<Application>();
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
                }

                appsList.ItemsSource = appsLib;
            }
        }

        private void appsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void deleteFromFriendsButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}