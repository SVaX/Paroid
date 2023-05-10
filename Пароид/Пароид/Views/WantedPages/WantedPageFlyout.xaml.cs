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
    public partial class WantedPageFlyout : ContentPage
    {
        public ListView ListView;

        public WantedPageFlyout()
        {
            InitializeComponent();

            BindingContext = new WantedPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class WantedPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<WantedPageFlyoutMenuItem> MenuItems { get; set; }

            public WantedPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<WantedPageFlyoutMenuItem>(new[]
                {
                    new WantedPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new WantedPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new WantedPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new WantedPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new WantedPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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