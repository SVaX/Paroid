﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Пароид.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminAppsPage : FlyoutPage
    {
        public AdminAppsPage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as AdminAppsPageFlyoutMenuItem;
            if (item == null)
                return;

            if (item.Title == "Пользователи")
            {
                await Navigation.PushModalAsync(new AdminUsersPage());
            }
            else if (item.Title == "Приложения")
            {
                await Navigation.PushModalAsync(new AdminAppsPage());
            }
            else if (item.Title == "Комментарии")
            {
                await Navigation.PushModalAsync(new AdminCommentsPage());
            }
        }
    }
}