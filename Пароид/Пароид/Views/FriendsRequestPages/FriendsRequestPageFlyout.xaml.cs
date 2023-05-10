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
    public partial class FriendsRequestPageFlyout : ContentPage
    {
        public ListView ListView;

        public FriendsRequestPageFlyout()
        {
            InitializeComponent();

            BindingContext = new FriendsRequestPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FriendsRequestPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FriendsRequestPageFlyoutMenuItem> MenuItems { get; set; }

            public FriendsRequestPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FriendsRequestPageFlyoutMenuItem>(new[]
                {
                    new FriendsRequestPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new FriendsRequestPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new FriendsRequestPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new FriendsRequestPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new FriendsRequestPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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