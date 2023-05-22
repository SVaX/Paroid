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
    public partial class AdminAppsPageFlyout : ContentPage
    {
        public ListView ListView;

        public AdminAppsPageFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminAppsPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AdminAppsPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminAppsPageFlyoutMenuItem> MenuItems { get; set; }

            public AdminAppsPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminAppsPageFlyoutMenuItem>(new[]
                {
                    new AdminAppsPageFlyoutMenuItem { Id = 0, Title = "Пользователи" },
                    new AdminAppsPageFlyoutMenuItem { Id = 1, Title = "Приложения" },
                    new AdminAppsPageFlyoutMenuItem { Id = 2, Title = "Комментарии" },
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