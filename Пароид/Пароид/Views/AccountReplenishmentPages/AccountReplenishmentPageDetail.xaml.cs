using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountReplenishmentPageDetail : ContentPage
    {
        public AccountReplenishmentPageDetail()
        {
            InitializeComponent();
            var user = GetUser();
            onBalanceTextBox.Text = user.Balance.ToString();
        }

        private async void payButton_Clicked(object sender, EventArgs e)
        {
            UpdateUserBalance();
            DisplayAlert("Успешно!", "Успешно пополнен баланс!", "OK");
            await Navigation.PushModalAsync(new MainMenuPage());
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

        private void UpdateUserBalance()
        {
            var user = GetUser();
            var balance = user.Balance + int.Parse(addTextBox.Text);
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
        }
    }
}