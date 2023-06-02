using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Пароид.Models;
using Application = Пароид.Models.Application;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPageDetail : ContentPage
    {
        List<Cart> _carts = new List<Cart>();
        List<Application> apps = new List<Application>();
        Application selectedapp = new Application();
        int cost = 0;
        public CartPageDetail()
        {
            InitializeComponent();
            getCarts();
            getApps();
            costTextBox.Text = $"Всего: {cost} руб.";
            if (cost == 0)
            {
                buyButton.IsEnabled = false;
            }
        }

        private void appsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            removeButton.IsEnabled = true;
            selectedapp = appsList.SelectedItem as Application;
        }

        private async void buyButton_Clicked(object sender, EventArgs e)
        {
            var user = GetUser();
            if (user.Balance > cost)
            {
                AddToLibrary();
                string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
                string databaseTable = "Cart";
                string selectQuery = $"DELETE FROM {databaseTable} WHERE Id_App = {selectedapp.AppId} AND Id_User = {int.Parse(Preferences.Get("currentUserId", "0"))}";
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
                    UpdateUserBalance();
                }
                DisplayAlert("Успешно", "Приложение/я успешно куплены!", "ОК");
                await Navigation.PushModalAsync(new MainMenuPage());
                return;
            }
            else
            {
                Preferences.Set("cartCost", cost.ToString());
                await Navigation.PushModalAsync(new PaymentPage());
            }
            
        }

        private void UpdateUserBalance()
        {
            var user = GetUser();
            var balance = user.Balance - cost;
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
            string selectQuery = $"UPDATE {databaseTable} SET Balance = {balance} WHERE UserId = {user.UserId}";
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
            }
            DisplayAlert("Успешно", "Пользователь добавлен в друзья", "ОК");
        }

        private void AddToLibrary()
        {
            foreach (var app in apps)
            {
                var cart = new Library();
                cart.IdApp = app.AppId;
                cart.IdUser = int.Parse(Preferences.Get("currentUserId", "0"));
                string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
                string databaseTable = "Library";
                string selectQuery = $"INSERT INTO {databaseTable} (Id_App, Id_User) VALUES ({cart.IdApp}, {cart.IdUser})";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteNonQuery();
                }

                
                
            }
            
        }
    

        private User GetUser()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE UserId = {1}", databaseTable, int.Parse(Preferences.Get("currentUserId", "0")));
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
                    var user = new User();
                    user.UserId = int.Parse(result[0].ToString());
                    user.Login = result[1].ToString();
                    user.Password = result[2].ToString();
                    user.Balance = int.Parse(result[3].ToString());
                    user.PermissionLevel = result[4].ToString();
                    user.Email = result[5].ToString();
                    user.RegistrationDate = DateTime.Parse(result[6].ToString());
                    user.Description = result[7].ToString();
                    return user;
                }
                return null;
            }
        }

        private void removeButton_Clicked(object sender, EventArgs e)
        {

            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Cart";
            string selectQuery = $"DELETE FROM {databaseTable} WHERE Id_App = {selectedapp.AppId} AND Id_User = {int.Parse(Preferences.Get("currentUserId", "0"))}";
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

                getCarts();
                getApps();
                appsList.ItemsSource = null;
                appsList.ItemsSource = apps;
            }
            DisplayAlert("Успешно", "Приложение удалено из корзины", "ОК");
        }

        private void getApps()
        {
            apps.Clear();
            cost = 0;
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
                List<Application> appsd = new List<Application>();
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
                    appsd.Add(app);
                }

                foreach (var app in appsd)
                {
                    foreach (var lib in _carts)
                    {
                        if (app.AppId == lib.IdApp)
                        {
                            apps.Add(app);
                            cost += app.Cost;
                        }
                    }
                }

                appsList.ItemsSource = apps;
            }
        }

        private void getCarts()
        {
            _carts.Clear();
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Cart";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_User = {1}", databaseTable, int.Parse(Preferences.Get("currentUserId", "0")));
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
    }
}