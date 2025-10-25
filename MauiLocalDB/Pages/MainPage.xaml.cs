using MauiLocalDB.Models;
using MauiLocalDB.PageModels;

namespace MauiLocalDB.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}