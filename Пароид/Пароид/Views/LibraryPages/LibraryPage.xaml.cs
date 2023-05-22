using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryPage : FlyoutPage
    {
        public LibraryPage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as LibraryPageFlyoutMenuItem;
            if (item == null)
                return;

            if (item.Title == "Магазин")
            {
                await Navigation.PushModalAsync(new MainMenuPage());
            }
            else if (item.Title == "Библиотека")
            {
                return;
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