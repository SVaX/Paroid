using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountReplenishmentPageFlyout : ContentPage
    {
        public ListView ListView;

        public AccountReplenishmentPageFlyout()
        {
            InitializeComponent();

            BindingContext = new AccountReplenishmentPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AccountReplenishmentPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AccountReplenishmentPageFlyoutMenuItem> MenuItems { get; set; }

            public AccountReplenishmentPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AccountReplenishmentPageFlyoutMenuItem>(new[]
                {
                    new AccountReplenishmentPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new AccountReplenishmentPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new AccountReplenishmentPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new AccountReplenishmentPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new AccountReplenishmentPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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