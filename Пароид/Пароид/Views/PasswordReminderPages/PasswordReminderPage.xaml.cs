using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Пароид.Models;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Net.Http.Json;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordReminderPage : ContentPage
    {
        int _operationCode = 0;
		/// <summary>
		/// Существование пользователя.
		/// </summary>
		private bool _userExists = false;

		/// <summary>
		/// Пользователь.
		/// </summary>
		private User _user;
		/// <summary>
		/// Код восстановления.
		/// </summary>
		private int _restorationCode;
		public PasswordReminderPage()
        {
            InitializeComponent();
        }

        private async void codeButton_Clicked(object sender, EventArgs e)
        {
			#region Валидация.
			if (_operationCode == 0 && String.IsNullOrEmpty(emailTextBox.Text))
			{
				await DisplayAlert("Введите почту!", "", "ok");
				return;
			}

			if (_operationCode == 1 && String.IsNullOrEmpty(assuranceCodeTextBox.Text))
			{
				await DisplayAlert("Неверный код!", "Введенный код был неверным", "ok");
				return;
			}

			if (_operationCode == 2 && String.IsNullOrEmpty(passwordTextBox.Text) && String.IsNullOrEmpty(repeatPasswordTextBox.Text) && passwordTextBox.Text == repeatPasswordTextBox.Text)
			{
				await DisplayAlert("Пароли должны совпадать!", "Введенный пароль был пустым или не совпал!", "ok");
				return;
			}
			var httpClientHandler = new HttpClientHandler()
			{
				UseDefaultCredentials = true
			};
			httpClientHandler.ServerCertificateCustomValidationCallback = (senderv, cert, chain, sslPolicyErrors) => { return true; };
			var requestUri = "https://192.168.1.69:7184/api/Users";

			using (var httpClient = new HttpClient(httpClientHandler))
			{
				var getResponse = httpClient.GetAsync(requestUri);

				var result = await getResponse.Result.Content.ReadAsStringAsync();
				List<User> usersList = JsonConvert.DeserializeObject<List<User>>(result);
				foreach (var user in usersList)
				{
					if (emailTextBox.Text == user.Email)
					{
						_userExists = true;
						_user = user;
					}
				}
				if (!_userExists)
                {
					await DisplayAlert("Неверная почта!", "Введенная почта была неверной", "ok");
					return;
				}
				if (_operationCode == 0)
				{
					try
					{
						SendCode();
						emailTextBox.IsEnabled = false;
						codeButton.Text = "Ввести код";
						codeLabel.IsVisible = true;
						codeFrame.IsVisible = true;
						_operationCode = 1;
					}
					catch (Exception ex)
					{
						await DisplayAlert("Упс!", "Что-то пошло не так, повторите попытку позже", "ok");
						return;
					}
				}
				else if (_operationCode == 1)
				{
					if (_restorationCode == int.Parse(assuranceCodeTextBox.Text))
					{
						codeFrame.IsEnabled = false;
						codeButton.Text = "Подтвердить пароль";
						passwordFrame.IsVisible = true;
						passagainFrame.IsVisible = true;
						_operationCode = 2;
					}
				}
				else if (_operationCode == 2)
				{
					_user.Password = passwordTextBox.Text;
					var content = JsonContent.Create(_user);
					var putResponse = httpClient.PutAsync(requestUri + "/" + _user.UserId, content);
					var userUpdated = putResponse.Result.Content.ReadAsStringAsync().Result;
					if (userUpdated == "true")
					{
						await DisplayAlert("Пароль успешно изменен!", "Пароль был изменен!", "ok");
						loginTap_Tapped(sender, e);
						return;
					}
					
				}
			}
			#endregion
		}
		private void SendCode()
		{
			MailAddress from = new MailAddress("dotacourierentertainment@mail.ru", "Bloodseeker");
			MailAddress to = new MailAddress(emailTextBox.Text);
			MailMessage m = new MailMessage(from, to);
			m.Subject = "Use code to restore your password";

			if (_userExists)
			{
				var rand = new Random();
				_restorationCode = rand.Next(900000, 1000000);
				m.Body = "<h1>Code to new password: " + _restorationCode + "</h1>";
			}

			m.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
			smtp.Credentials = new NetworkCredential("dotacourierentertainment@mail.ru", "e7hQDMWEpQV5yuCBXceS");
			smtp.EnableSsl = true;
			smtp.Send(m);
		}

		private  async void loginTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void newAccTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegistrationPage());
        }
    }
}