using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Пароид.Models;
using Application = Пароид.Models.Application;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePageDetail : ContentPage
    {
        List<Library> libs = new List<Library>();
        User selectedUser = new User();
        User currentUser = new User();
        public ProfilePageDetail()
        {
            InitializeComponent();
            currentUser = GetCurrentUser();
            selectedUser = GetSelectedUser();
            if (currentUser.UserId == selectedUser.UserId)
            {
                addToFriendsButton.IsVisible = false;
                descriptionText.IsReadOnly = false;
                saveDescription.IsVisible = true;
                friendsList.IsVisible = true;
                if (currentUser.PermissionLevel == "Developer")
                {
                    becomeDeveloperButton.IsVisible = false;
                    addAppButton.IsVisible = true;
                }
                else if (currentUser.PermissionLevel == "Client")
                {
                    becomeDeveloperButton.IsVisible = true;
                    addAppButton.IsVisible = false;
                }
            }
            else
            {
                addToFriendsButton.IsVisible = !friendshipExist();
                descriptionText.IsReadOnly = true;
                saveDescription.IsVisible = false;
                becomeDeveloperButton.IsVisible = false;
                addAppButton.IsVisible = false;
                friendsList.IsVisible = false;
            }

            userName.Text = selectedUser.Login;
            descriptionText.Text = selectedUser.Description;
            getLibraries();
        }

        private void getLibraries()
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
            }
            appsOwnedLabel.Text = "Apps Owned: " + libs.Count;
            regDateLabel.Text = "on Пар since " + selectedUser.RegistrationDate.ToShortDateString();
        }

        private bool friendshipExist()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Friendship";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE (Id_User1 = {1} OR Id_User2 = {1}) AND (Id_User1 = {2} OR Id_User2 = {2})", databaseTable, Preferences.Get("currentUserId", "0"), selectedUser.UserId.ToString());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteReader();
                //check if account exists
                bool exist = result.HasRows;
                return exist;
            }
        }

        private void addToFriendsButton_Clicked(object sender, EventArgs e)
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Friendship";
            string selectQuery = String.Format("INSERT INTO {0} (Id_User1, Id_User2, Confirmed) VALUES ({1}, {2}, 0)", databaseTable, Preferences.Get("currentUserId", "0"), selectedUser.UserId.ToString());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteReader();
                DisplayAlert("Успешно!", "Пользователь добавлен в друзья!", "ОК!");
                return;
                //check if account exists
            }
            addToFriendsButton.IsVisible = false;
        }

        private User GetSelectedUser()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Login='{1}'", databaseTable, Preferences.Get("selectedUserName", "0"));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteReader();
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
                    Preferences.Set("selectedUserName", Preferences.Get("_currentUserName", "0"));
                    return user;
                }
                return null;
            }
            
        }

        private User GetCurrentUser()
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE UserId={1}", databaseTable, (Preferences.Get("currentUserId", "0")));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteReader();
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

        private async void friendsList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FriendsPage());
        }

        private async void becomeDeveloperButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BecomeDeveloperPage());
        }

        private async void addAppButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddAppPage());
        }

        private void saveDescription_Clicked(object sender, EventArgs e)
        {
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[User]";
            string selectQuery = String.Format("UPDATE {0} SET Description= '{1}' WHERE UserId={2}", databaseTable, descriptionText.Text, (Preferences.Get("currentUserId", "0")));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteNonQuery();
                DisplayAlert("Успешно!", "Описание было изменено!", "ОК!");
                return;
            }
        }
    }
}