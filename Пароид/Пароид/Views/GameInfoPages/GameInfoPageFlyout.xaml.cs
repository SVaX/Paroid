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

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameInfoPageFlyout : ContentPage
    {
        public ListView ListView;

        public GameInfoPageFlyout()
        {
            InitializeComponent();

            BindingContext = new GameInfoPageFlyoutViewModel();
            ListView = MenuItemsListView;
            usernameLabel.Text = Preferences.Get("_currentUserName", "unknown");
        }

        private class GameInfoPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<GameInfoPageFlyoutMenuItem> MenuItems { get; set; }

            public GameInfoPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<GameInfoPageFlyoutMenuItem>(new[]
                {
                    new GameInfoPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new GameInfoPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new GameInfoPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new GameInfoPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new GameInfoPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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