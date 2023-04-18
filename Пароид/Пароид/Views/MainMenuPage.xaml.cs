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
    public partial class MainMenuPage : FlyoutPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuPageFlyoutMenuItem;
            if (item == null)
                return;

            if(item.Title == "Желаемое")
            {
                await Navigation.PushModalAsync(new WantedPage());
            }
            else if(item.Title == "Библиотека")
            {
                await Navigation.PushModalAsync(new LibraryPage());
            }
            //Detail = new NavigationPage(page);
            //IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}