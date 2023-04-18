﻿using System;
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
    public partial class AdminUsersPageFlyout : ContentPage
    {
        public ListView ListView;

        public AdminUsersPageFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminUsersPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AdminUsersPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminUsersPageFlyoutMenuItem> MenuItems { get; set; }

            public AdminUsersPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminUsersPageFlyoutMenuItem>(new[]
                {
                    new AdminUsersPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new AdminUsersPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new AdminUsersPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new AdminUsersPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new AdminUsersPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
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