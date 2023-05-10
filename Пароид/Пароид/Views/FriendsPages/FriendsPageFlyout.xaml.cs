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
    public partial class FriendsPageFlyout : ContentPage
    {
        public ListView ListView;

        public FriendsPageFlyout()
        {
            InitializeComponent();

            BindingContext = new FriendsPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FriendsPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FriendsPageFlyoutMenuItem> MenuItems { get; set; }

            public FriendsPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FriendsPageFlyoutMenuItem>(new[]
                {
                    new FriendsPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new FriendsPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new FriendsPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new FriendsPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new FriendsPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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