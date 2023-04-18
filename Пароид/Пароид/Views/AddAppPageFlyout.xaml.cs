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
    public partial class AddAppPageFlyout : ContentPage
    {
        public ListView ListView;

        public AddAppPageFlyout()
        {
            InitializeComponent();

            BindingContext = new AddAppPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AddAppPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AddAppPageFlyoutMenuItem> MenuItems { get; set; }

            public AddAppPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AddAppPageFlyoutMenuItem>(new[]
                {
                    new AddAppPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new AddAppPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new AddAppPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new AddAppPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new AddAppPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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