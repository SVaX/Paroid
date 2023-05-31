using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Пароид.Models.Application;
using Пароид.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryPageDetail : ContentPage
    {
        List<Application> appsLib = new List<Application>();
        List<Library> libs = new List<Library>();
        public LibraryPageDetail()
        {
            InitializeComponent();
            getLibraries();
        }

        private async void appsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var app = e.Item as Application;
            Preferences.Set("selectedAppName", app.Name);
            Preferences.Set("selectedAppId", app.AppId.ToString());
            await Navigation.PushModalAsync(new GameInfoPage());
        }

        private async void getLibraries()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Library";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_User = {1}", databaseTable, Preferences.Get("currentUserId", "0"));
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
                while (result.Read())
                {
                    var library = new Library();
                    library.LibraryId = int.Parse(result[0].ToString());
                    library.IdUser = int.Parse(result[1].ToString());
                    library.IdApp = int.Parse(result[2].ToString());
                    libs.Add(library);
                }
                getApps();
            }

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

                foreach (var app in apps)
                {
                    foreach (var lib in libs)
                    {
                        if (app.AppId == lib.IdApp)
                        {
                            appsLib.Add(app);
                        }
                    }
                }

                appsList.ItemsSource = appsLib;
            }
        }
    }
}