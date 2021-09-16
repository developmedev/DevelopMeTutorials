using DevelopMeTutorials.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DevelopMeTutorials.Views
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