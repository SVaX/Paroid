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
    public partial class CartPageFlyout : ContentPage
    {
        public ListView ListView;

        public CartPageFlyout()
        {
            InitializeComponent();

            BindingContext = new CartPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class CartPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CartPageFlyoutMenuItem> MenuItems { get; set; }

            public CartPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<CartPageFlyoutMenuItem>(new[]
                {
                    new CartPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new CartPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new CartPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new CartPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new CartPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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