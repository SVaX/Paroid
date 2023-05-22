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
    public partial class PasswordReminderPage : ContentPage
    {
        public PasswordReminderPage()
        {
            InitializeComponent();
        }

        private void codeButton_Clicked(object sender, EventArgs e)
        {

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