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
    public partial class GameInfoPageFlyout : ContentPage
    {
        public ListView ListView;

        public GameInfoPageFlyout()
        {
            InitializeComponent();

            BindingContext = new GameInfoPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class GameInfoPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<GameInfoPageFlyoutMenuItem> MenuItems { get; set; }

            public GameInfoPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<GameInfoPageFlyoutMenuItem>(new[]
                {
                    new GameInfoPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new GameInfoPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new GameInfoPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new GameInfoPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new GameInfoPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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