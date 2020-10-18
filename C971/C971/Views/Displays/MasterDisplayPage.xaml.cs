using C971.Models;
using C971.ViewModel;
using System;
using Xamarin.Forms;

namespace C971.Views
{
    public partial class MasterDisplayPage : ContentPage
    {
        private MasterDisplayViewModel viewModel;

        public MasterDisplayPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MasterDisplayViewModel();
        }

        private async void Add_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddModifyTermPage()));
        }

        private async void Term_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            Term term = args.SelectedItem as Term;
            if (term == null)
            { return; }
            await Navigation.PushAsync(new TermDisplayPage(new TermDisplayViewModel(term)));

            TermsListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            if (MasterDisplayViewModel.FirstLaunch)
            {
                string notificationMessage = await viewModel.DateNotifications();
                if (MasterDisplayViewModel.HasNotifications)
                {
                    await DisplayAlert("Important Notifications", notificationMessage, "Ok");
                }
                MasterDisplayViewModel.FirstLaunch = false;
            }
            if (viewModel.Terms.Count == 0)
                viewModel.LoadTermsCommand.Execute(null);
        }
    }
}