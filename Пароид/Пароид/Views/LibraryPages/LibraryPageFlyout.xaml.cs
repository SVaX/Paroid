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
    public partial class LibraryPageFlyout : ContentPage
    {
        public ListView ListView;

        public LibraryPageFlyout()
        {
            InitializeComponent();

            BindingContext = new LibraryPageFlyoutViewModel();
            ListView = MenuItemsListView;
            userNameLabel.Text = Preferences.Get("_currentUserName", "default_value");
        }

        private class LibraryPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<LibraryPageFlyoutMenuItem> MenuItems { get; set; }

            public LibraryPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<LibraryPageFlyoutMenuItem>(new[]
                {
                    new LibraryPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new LibraryPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new LibraryPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new LibraryPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new LibraryPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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