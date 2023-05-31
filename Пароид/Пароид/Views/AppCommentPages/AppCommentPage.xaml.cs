using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Пароид.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppCommentPage : ContentPage
    {

        public AppCommentPage()
        {
            InitializeComponent();
            Title.Text = "Комментарии для " + Preferences.Get("selectedAppName", "defaultValue");
        }

        private async void backButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GameInfoPage());
        }

        private void confirmButton_Tapped(object sender, EventArgs e)
        {

        }

        private void ratingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ratingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
