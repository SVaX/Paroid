using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Пароид.Models;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void loginTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var email = emailTextBox.Text;
            var password = passwordTextBox.Text;
            var passwordAgain = repeatPasswordTextBox.Text;

            if (password != passwordAgain)
            {
                await DisplayAlert("Предупреждение", "Пароли не совпадают!", "ОК");
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
                var getResponse = httpClient.GetAsync(requestUri + "/DoesUserExist" + "?username=" + login);
                var getResponses = getResponse.Result.Content.ReadAsStringAsync();
                var userExists = getResponses.Result;

                if (userExists == "true")
                {
                    await DisplayAlert("Такой пользователь уже существует!", "Придумайте другое имя пользователя", "ok");
                    return;
                }

                var newUser = new User();
                newUser.Login = login;
                newUser.Password = password;
                newUser.Email = email;
                newUser.RegistrationDate = DateTime.Now;
                newUser.Balance = 0;
                newUser.Description = "";
                newUser.PermissionLevel = "Client";

                var content = JsonContent.Create(newUser);
                var response = await httpClient.PostAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Уcпешно!", "Регистрация прошла успешно!", "ok");
                    loginTap_Tapped(sender, e);
                    return;
                }
            }
        }

        private async void chooseAvatarButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Извините!", "Функция пока не доступна!", "OK");
        }
    }
}