using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    public partial class AddAppPageDetail : ContentPage
    {
        public AddAppPageDetail()
        {
            InitializeComponent();
        }

        private async void addButton_Clicked(object sender, EventArgs e)
        {
            var name = nameTextBox.Text;
            var email = emailTextBox.Text;
            var price = int.Parse(priceTextBox.Text);
            var file = Encoding.ASCII.GetBytes(installerTextBox.Text);
            var inst = Encoding.ASCII.GetBytes(imageTextBox.Text);
            string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
            string selectQuery = $"INSERT INTO Application (Name, Description, Rating, Cost, [File], Picture) VALUES ('{name}', '{email}', 0, {price}, {"0x" + BitConverter.ToString(file).Replace("-", "")}, {"0x" + BitConverter.ToString(inst).Replace("-", "")})";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //open connection
                connection.Open();

                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Connection = connection;
                command.CommandText = selectQuery;
                var result = command.ExecuteNonQuery();
                DisplayAlert("Успешно", "Приложение добавлено", "ОК");
            }

            
            await Navigation.PushModalAsync(new MainMenuPage());
        }
    }
}