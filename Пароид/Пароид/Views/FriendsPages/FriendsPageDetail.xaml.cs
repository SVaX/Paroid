using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Пароид.Models;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPageDetail : ContentPage
    {
        List<Friendship> friendList = new List<Friendship>();
        List<User> userList = new List<User>();
        public FriendsPageDetail()
        {
            InitializeComponent();
            GetFriendships();
            GetFriendsUser();
            friendsList.ItemsSource = userList;
        }

        private async void friendsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var usr = friendsList.SelectedItem as User;
            Preferences.Set("selectedUserName", usr.Login);
            await Navigation.PushModalAsync(new ProfilePage());
        }

        private void deleteFromFriendsButton_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // Find the single item here
            User product = btn.BindingContext as User;

            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Friendship";
            string selectQuery = $"UPDATE {databaseTable} SET Confirmed = 0 WHERE (Id_User1 = {Preferences.Get("currentUserId", "0")} OR Id_User2 = {Preferences.Get("currentUserId", "0")}) AND (Id_User1 = {product.UserId} OR Id_User2 = {product.UserId})";
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

                GetFriendships();
                GetFriendsUser();
                friendsList.ItemsSource = null;
                friendsList.ItemsSource = userList;
            }
            DisplayAlert("Успешно", "Пользователь удален из друзей", "ОК");
        }

        private void GetFriendships()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Friendship";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE (Id_User1 = {1} OR Id_User2 = {1}) AND Confirmed = 1", databaseTable, Preferences.Get("currentUserId", "0"));
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
                friendList.Clear();
                while (result.Read())
                {
                    
                    var friend = new Friendship();
                    friend.FriendshipId = int.Parse(result[0].ToString());
                    friend.IdUser1 = int.Parse(result[1].ToString());
                    friend.IdUser2 = int.Parse(result[2].ToString());
                    friend.Confirmed = bool.Parse(result[3].ToString());
                    friendList.Add(friend);
                }
            }
        }

        private void GetFriendsUser()
        {
            userList.Clear();
            foreach (var friend in friendList)
            {
                if (friend.IdUser1.ToString() == Preferences.Get("currentUserId", "0"))
                {
                    var usId = friend.IdUser2;
                    string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
                    string databaseTable = "[User]";
                    string selectQuery = String.Format("SELECT * FROM {0} WHERE UserId = {1}", databaseTable, usId.ToString());
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
                            userList.Add(user);
                        }
                    }
                }
                else if (friend.IdUser2.ToString() == Preferences.Get("currentUserId", "0"))
                {
                    var usId = friend.IdUser1;
                    string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
                    string databaseTable = "[User]";
                    string selectQuery = String.Format("SELECT * FROM {0} WHERE UserId = {1}", databaseTable, usId.ToString());
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
                            userList.Add(user);
                        }
                    }
                }
            }
        }
    }
}