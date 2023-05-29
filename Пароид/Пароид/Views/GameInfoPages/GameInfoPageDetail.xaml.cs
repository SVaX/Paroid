using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameInfoPageDetail : ContentPage
    {
        public GameInfoPageDetail()
        {
            InitializeComponent();
            this.Title = Preferences.Get("selectedAppName", "App");
        }
    }
}