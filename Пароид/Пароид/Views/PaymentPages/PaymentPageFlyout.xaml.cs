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
    public partial class PaymentPageFlyout : ContentPage
    {
        public ListView ListView;

        public PaymentPageFlyout()
        {
            InitializeComponent();

            BindingContext = new PaymentPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class PaymentPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<PaymentPageFlyoutMenuItem> MenuItems { get; set; }

            public PaymentPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<PaymentPageFlyoutMenuItem>(new[]
                {
                    new PaymentPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new PaymentPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new PaymentPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new PaymentPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new PaymentPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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