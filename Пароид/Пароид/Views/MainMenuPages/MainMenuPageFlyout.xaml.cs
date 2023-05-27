using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPageFlyout : ContentPage
    {
        public ListView ListView;
        User _currentUser = new User();
        
        public MainMenuPageFlyout()
        {
            InitializeComponent();
            BindingContext = new MainMenuPageFlyoutViewModel();
            ListView = MenuItemsListView;
            GetUserName();
            
        }

        private async void GetUserName()
        {
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
                    if (user.Login == Preferences.Get("_currentUserName", "default_value"))
                    {
                        _currentUser = user;
                    }
                }
                usernameLabel.Text = _currentUser.Login;
            }
        }

        private class MainMenuPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMenuPageFlyoutMenuItem> MenuItems { get; set; }

            public MainMenuPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainMenuPageFlyoutMenuItem>(new[]
                {
                    new MainMenuPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new MainMenuPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new MainMenuPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new MainMenuPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new MainMenuPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}