using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAppPageFlyout : ContentPage
    {
        public ListView ListView;

        public AddAppPageFlyout()
        {
            InitializeComponent();

            BindingContext = new AddAppPageFlyoutViewModel();
            ListView = MenuItemsListView;
            userNameLabel.Text = Preferences.Get("_currentUserName", "default_value");
        }

        private class AddAppPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AddAppPageFlyoutMenuItem> MenuItems { get; set; }

            public AddAppPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AddAppPageFlyoutMenuItem>(new[]
                {
                    new AddAppPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new AddAppPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new AddAppPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new AddAppPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new AddAppPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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