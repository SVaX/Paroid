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
    public partial class GameInfoPage : ContentPage
    {
        public GameInfoPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ShopPage());
        }
    }
}