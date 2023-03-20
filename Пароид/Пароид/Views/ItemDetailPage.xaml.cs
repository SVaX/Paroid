using System.ComponentModel;
using Xamarin.Forms;
using Пароид.ViewModels;

namespace Пароид.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}