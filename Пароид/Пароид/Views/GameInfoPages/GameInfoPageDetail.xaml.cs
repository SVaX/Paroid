using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Пароид.Models;
using Application = Пароид.Models.Application;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameInfoPageDetail : ContentPage
    {
        List<Cart> _carts = new List<Cart>();
        List<Wanted> _wanteds = new List<Wanted>();
        List<Library> _libraries = new List<Library>();
        public GameInfoPageDetail()
        {
            InitializeComponent();
            this.Title = Preferences.Get("selectedAppName", "App");
            var app = getAppInfo();
            appName.Text = app.Name;
            ratingLabel.Text = "Оценка: " + app.Rating.ToString();
            descriptionText.Text = app.Description;
            costEditor.Text = "Цена: " + app.Cost.ToString();
            getCarts();
            getWanted();
            getLibraries();
            if (_carts.Count == 0)
            {
                cartButton.IsEnabled = true;
                cartButton.Text = "Добавить в корзину";
            }
            if (_wanteds.Count == 0)
            {
                wantedButton.IsEnabled = true;
                wantedButton.Text = "В желаемое";
            }
            if (_libraries.Count != 0)
            {
                cartButton.IsEnabled = false;
                cartButton.Text = "Уже приобретено!";
                wantedButton.IsEnabled = false;
                wantedButton.Text = "Уже приобретено!";
            }
        }

        private void getCarts()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Cart";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_App= {1} AND Id_User = {2}", databaseTable, int.Parse(Preferences.Get("selectedAppId", "0")), int.Parse(Preferences.Get("currentUserId", "0")));
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
                var carts = new List<Cart>();
                while (result.Read())
                {
                    var cart = new Cart();
                    cart.CartId = int.Parse(result[0].ToString());
                    cart.IdUser = int.Parse(result[1].ToString());
                    cart.IdApp = int.Parse(result[2].ToString());
                    carts.Add(cart);
                }
                _carts = carts;
            }

        }

        private void getLibraries()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Library";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_App= {1} AND Id_User = {2}", databaseTable, int.Parse(Preferences.Get("selectedAppId", "0")), int.Parse(Preferences.Get("currentUserId", "0")));
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
                var libs = new List<Library>();
                while (result.Read())
                {
                    var library = new Library();
                    library.LibraryId = int.Parse(result[0].ToString());
                    library.IdUser = int.Parse(result[1].ToString());
                    library.IdApp = int.Parse(result[2].ToString());
                    libs.Add(library);
                }
                _libraries = libs;
            }

        }

        private void getWanted()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Wanted";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_App= {1} AND Id_User = {2}", databaseTable, int.Parse(Preferences.Get("selectedAppId", "0")), int.Parse(Preferences.Get("currentUserId", "0")));
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
                var carts = new List<Wanted>();
                while (result.Read())
                {
                    var cart = new Wanted();
                    cart.WantedId = int.Parse(result[0].ToString());
                    cart.IdUser = int.Parse(result[1].ToString());
                    cart.IdApp = int.Parse(result[2].ToString());
                    cart.DateAdded = DateTime.Parse(result[3].ToString());
                    carts.Add(cart);
                }
                _wanteds = carts;
            }
        }

        private Application getAppInfo()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Application";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE AppId= {1}", databaseTable, int.Parse(Preferences.Get("selectedAppId", "0")));
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
                    var app = new Application();
                    app.AppId = int.Parse(result[0].ToString());
                    app.Name = result[1].ToString();
                    app.Picture = Encoding.UTF8.GetBytes(result[2].ToString());
                    app.Description = result[3].ToString();
                    app.File = Encoding.ASCII.GetBytes(result[4].ToString());
                    app.Rating = int.Parse(result[5].ToString());
                    app.Cost = int.Parse(result[6].ToString());
                    return app;
                }
                return null;
            }
        }

        private async void commentsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AppCommentPage());
        }

        private void cartButton_Clicked(object sender, EventArgs e)
        {
            var cart = new Cart();
            cart.IdApp = int.Parse(Preferences.Get("selectedAppId", "0"));
            cart.IdUser = int.Parse(Preferences.Get("currentUserId", "0"));
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Cart";
            string selectQuery = String.Format("INSERT INTO {0} (Id_App, Id_User) VALUES ({1}, {2})", databaseTable, cart.IdApp, cart.IdUser);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteNonQuery();
            }

            DisplayAlert("Успешно", "Приложение добавлено в корзину!", "ОК");
            cartButton.IsEnabled = false;
            return;
        }

        private void wantedButton_Clicked(object sender, EventArgs e)
        {
            var cart = new Cart();
            cart.IdApp = int.Parse(Preferences.Get("selectedAppId", "0"));
            cart.IdUser = int.Parse(Preferences.Get("currentUserId", "0"));
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Wanted";
            string selectQuery = $"INSERT INTO {databaseTable} (Id_App, Id_User, DateAdded) VALUES ({cart.IdApp}, {cart.IdUser}, '{DateTime.Now.ToString("yyyy/MM/dd")}')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteNonQuery();
            }

            DisplayAlert("Успешно", "Приложение добавлено в желаемое!", "ОК");
            wantedButton.IsEnabled = false;
            return;
        }
    }
}