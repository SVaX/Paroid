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
    public partial class AdminUsersPage : FlyoutPage
    {
        public AdminUsersPage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as AdminUsersPageFlyoutMenuItem;
            if (item == null)
                return;

            if (item.Title == "Пользователи")
            {
                return;
            }
            else if (item.Title == "Приложения")
            {
                await Navigation.PushModalAsync(new AdminAppsPage());
            }
            else if (item.Title == "Комментарии")
            {
                await Navigation.PushModalAsync(new AdminCommentsPage());
            }
        }
    }
}