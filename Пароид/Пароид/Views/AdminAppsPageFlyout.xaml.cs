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
                    new AdminAppsPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new AdminAppsPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new AdminAppsPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new AdminAppsPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new AdminAppsPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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