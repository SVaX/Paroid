using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppCommentPage : ContentPage
    {
        List<Comment> comments = new List<Comment>();
        public AppCommentPage()
        {
            InitializeComponent();
            Title.Text = "Комментарии для " + Preferences.Get("selectedAppName", "defaultValue");
            GetComments();
            appsList.ItemsSource = comments;
        }

        private async void backButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GameInfoPage());
        }

        private void confirmButton_Tapped(object sender, EventArgs e)
        {
            var comment = commentTextBox.Text;
            var rating = int.Parse(ratingTextBox.Text);
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string selectQuery = $"INSERT INTO Comment (Id_User, Id_App, Date, CommentText, Score, IsReported) VALUES ({Preferences.Get("currentUserId", "0")}, {Preferences.Get("selectedAppId", "0")}, '{DateTime.Now}', '{comment}', {rating}, 0)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteNonQuery();
                DisplayAlert("Успешно", "Комментарий добавлен", "ОК");
                GetComments();
                appsList.ItemsSource = null;
                appsList.ItemsSource = comments;
            }
        }

        private void reportButton_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // Find the single item here
            Comment product = btn.BindingContext as Comment;

            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Comment";
            string selectQuery = $"UPDATE {databaseTable} SET IsReported = 1 WHERE CommentId = {product.CommentId}";
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

                GetComments();
                appsList.ItemsSource = null;
                appsList.ItemsSource = comments;
            }
            DisplayAlert("Успешно", "Комментарий зарепорчен", "ОК");
        }

        private void GetComments()
        {
            comments.Clear();
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[Comment]";
            string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_App={1} AND IsReported = 0", databaseTable, Preferences.Get("selectedAppId", "0"));
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
                    var comment = new Comment();
                    comment.CommentId = int.Parse(result[0].ToString());
                    comment.IdUser = int.Parse(result[1].ToString());
                    comment.IdApp = int.Parse(result[2].ToString());
                    comment.Date = DateTime.Parse(result[3].ToString());
                    comment.CommentText = result[4].ToString();
                    comment.Score = int.Parse(result[5].ToString());
                    comment.IsReported = bool.Parse(result[6].ToString());
                    comments.Add(comment);
                }
            }
        }

        private async void appsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var usr = appsList.SelectedItem as Comment;
            Preferences.Set("selectedUserName", usr.UserName);
            await Navigation.PushModalAsync(new ProfilePage());
        }
    }
}
