using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Пароид.Models;
using Пароид.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new LoginViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordTextBox.Text;

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
                var userExists = false;
                foreach (var user in usersList)
                {
                    if (user.Login == login)
                    {
                        if (user.Password == password)
                        {
                            if (user.PermissionLevel != "Admin")
                            {
                                Preferences.Set("_currentUserName", user.Login);
                                Preferences.Set("currentUserId", user.UserId.ToString());
                                Preferences.Set("selectedUserName", user.Login);
                                await Navigation.PushModalAsync(new MainMenuPage(user));
                            }
                            else
                            {
                                await Navigation.PushModalAsync(new AdminUsersPage());
                            }
                        }
                        else
                        {
                            userExists = true;
                            await DisplayAlert("Неверный пароль!", "Введенный пароль был неверным", "ok");
                            return;
                        }
                    }
                    else
                    {
                        userExists = false;
                    }
                }
                if (userExists)
                {
                    await DisplayAlert("Неверный логин!", "Введенный логин был неверным!", "ok");
                    return;
                }
            }
        } 

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegistrationPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PasswordReminderPage());
        }
    }
}