using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminUsersPageDetail : ContentPage
    {
        List<User> users = new List<User>();
        public AdminUsersPageDetail()
        {
            InitializeComponent();
            GetUsers();
        }

        private void deleteFriendships()
        {
            
        }

        private void deleteFromFriendsButton_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // Find the single item here
            User product = btn.BindingContext as User;

            string connectionString1 = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable1 = "Friendship";
            string selectQuery1 = $"DELETE FROM {databaseTable1} WHERE Id_User1 = {product.UserId} OR Id_User2 = {product.UserId}";
            using (SqlConnection connection = new SqlConnection(connectionString1))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery1, connection);

                command.Connection = connection;
                command.CommandText = selectQuery1;
                var result = command.ExecuteReader();
                //check if account exists
                var exists = result.HasRows;
            }

            string connectionString2= "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable11 = "Comment";
            string selectQuery11 = $"DELETE FROM {databaseTable11} WHERE Id_User = {product.UserId}";
            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery11, connection);

                command.Connection = connection;
                command.CommandText = selectQuery11;
                var result = command.ExecuteReader();
                //check if account exists
                var exists = result.HasRows;
            }

            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
            string selectQuery = $"DELETE FROM {databaseTable} WHERE UserId = {product.UserId}";
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
                GetUsers();
            }

            
            DisplayAlert("Успешно", "Пользователь удален", "ОК");
        }

        private void GetUsers()
        {
            users.Clear();
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
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
                    users.Add(user);
                }
            }
            friendsList.ItemsSource = null;
            friendsList.ItemsSource = users;
        }
    }
}