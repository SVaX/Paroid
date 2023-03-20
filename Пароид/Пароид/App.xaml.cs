using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Пароид.Services;
using Пароид.Views;

namespace Пароид
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
