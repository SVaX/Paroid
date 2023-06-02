using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Пароид.Models;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCommentsPageDetail : ContentPage
    {
        List<Comment> appsLib = new List<Comment>();
        public AdminCommentsPageDetail()
        {
            InitializeComponent();
            getApps();
        }

        private void getApps()
        {
            appsLib.Clear();
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "Comment";
            string selectQuery = $"SELECT * FROM {databaseTable} WHERE IsReported = 1";
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
                List<Comment> apps = new List<Comment>();
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
                    apps.Add(comment);
                }

                appsList.ItemsSource = apps;
            }
        }
        private void deleteFromFriendsButton_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // Find the single item here
            Comment product = btn.BindingContext as Comment;

            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string databaseTable = "[Comment]";
            string selectQuery = $"DELETE FROM {databaseTable} WHERE CommentId = {product.CommentId}";
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
                getApps();
            }


            DisplayAlert("Успешно", "Комментарий удален", "ОК");
        }
    }
}