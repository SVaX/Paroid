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
    public partial class AdminCommentsPageFlyout : ContentPage
    {
        public ListView ListView;

        public AdminCommentsPageFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminCommentsPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AdminCommentsPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminCommentsPageFlyoutMenuItem> MenuItems { get; set; }

            public AdminCommentsPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminCommentsPageFlyoutMenuItem>(new[]
                {
                    new AdminCommentsPageFlyoutMenuItem { Id = 0, Title = "Магазин" },
                    new AdminCommentsPageFlyoutMenuItem { Id = 1, Title = "Библиотека" },
                    new AdminCommentsPageFlyoutMenuItem { Id = 2, Title = "Желаемое" },
                    new AdminCommentsPageFlyoutMenuItem { Id = 3, Title = "Профиль" },
                    new AdminCommentsPageFlyoutMenuItem { Id = 4, Title = "Пополнить счет" },
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