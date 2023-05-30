using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Пароид.Models;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BecomeDeveloperPage : ContentPage
    {
		/// <summary>
		/// Код восстановления.
		/// </summary>
		private int _restorationCode;

		private int _operationCode = 0;

		public BecomeDeveloperPage()
        {
            InitializeComponent();
        }

		private async void sendCodeTextBox_Pressed(object sender, EventArgs e)
		{
			try
			{
				if (_operationCode == 1)
				{
					if (codeTextBox.Text == _restorationCode.ToString())
					{
						string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
						string databaseTable = "[User]";
						string selectQuery = String.Format("UPDATE {0} SET PermissionLevel= '{1}' WHERE UserId={2}", databaseTable, "Developer", int.Parse(Preferences.Get("currentUserId", "0")));
						using (SqlConnection connection = new SqlConnection(connectionString))
						{
							//open connection
							connection.Open();

							SqlCommand command = new SqlCommand(selectQuery, connection);

							command.Connection = connection;
							command.CommandText = selectQuery;
							var result = command.ExecuteNonQuery();
							await DisplayAlert("Успешно!", "Теперь вы разработчик!", "ОК!");
							await Navigation.PushModalAsync(new MainMenuPage());
							return;
						}
					}
				}
				else
				{
					SendCode();
					emailTextBox.IsEnabled = false;
					codeLabel.IsVisible = true;
					codeFrame.IsVisible = true;
					sendCodeTextBox.Text = "Подтвердить код";
					_operationCode = 1;
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Упс!", "Что-то пошло не так, повторите попытку позже", "ok");
				return;
			}
		}

		private User GetUser()
        {
			string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
			string databaseTable = "User";
			string selectQuery = String.Format("SELECT * FROM {0} WHERE UserId={2}", databaseTable, int.Parse(Preferences.Get("currentUserId", "0")));
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

		private void SendCode()
		{
			MailAddress from = new MailAddress("dotacourierentertainment@mail.ru", "Bloodseeker");
			MailAddress to = new MailAddress(emailTextBox.Text);
			MailMessage m = new MailMessage(from, to);
			m.Subject = "Use code to become developer";

			var rand = new Random();
			_restorationCode = rand.Next(900000, 1000000);
			m.Body = "<h1>Code to become developer: " + _restorationCode + "</h1>";

			m.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
			smtp.Credentials = new NetworkCredential("dotacourierentertainment@mail.ru", "e7hQDMWEpQV5yuCBXceS");
			smtp.EnableSsl = true;
			smtp.Send(m);
		}

		private async void backButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}