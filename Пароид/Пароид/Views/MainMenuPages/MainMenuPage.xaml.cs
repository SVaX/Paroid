using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : FlyoutPage
    {
        User _currentUser = new User();
        public MainMenuPage(User user = null)
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            if (user != null)
            {
                Preferences.Set("_currentUserName", user.Login);
                Preferences.Set("currentUserId", user.UserId.ToString());
                Preferences.Set("selectedUserName", user.Login);
            }
            _currentUser = user;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuPageFlyoutMenuItem;
            if (item == null)
                return;

            if (item.Title == "Магазин")
            {
                return;
            }
            else if (item.Title == "Библиотека")
            {
                await Navigation.PushModalAsync(new LibraryPage());
            }
            else if (item.Title == "Желаемое")
            {
                await Navigation.PushModalAsync(new WantedPage());
            }
            else if (item.Title == "Профиль")
            {
                await Navigation.PushModalAsync(new ProfilePage());
            }
            else if (item.Title == "Пополнить счет")
            {
                await Navigation.PushModalAsync(new AccountReplenishmentPage());
            }
        }
    }
}