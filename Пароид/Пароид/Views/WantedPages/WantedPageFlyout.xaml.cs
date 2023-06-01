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
    public partial class WantedPageFlyout : ContentPage
    {
        public ListView ListView;

        public WantedPageFlyout()
        {
            InitializeComponent();

            BindingContext = new WantedPageFlyoutViewModel();
            ListView = MenuItemsListView;
            userNameLabel.Text = Preferences.Get("_currentUserName", "default_value");
        }

        private class WantedPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<WantedPageFlyoutMenuItem> MenuItems { get; set; }

            public WantedPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<WantedPageFlyoutMenuItem>(new[]
                {
                    new WantedPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new WantedPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new WantedPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new WantedPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new WantedPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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