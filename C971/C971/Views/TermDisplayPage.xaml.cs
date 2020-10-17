using C971.Models;
using C971.ViewModel;
using System;
using Xamarin.Forms;

namespace C971.Views
{
    public partial class TermDisplayPage : ContentPage
    {
        private TermDisplayViewModel viewModel;

        public TermDisplayPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TermDisplayViewModel();
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
            await Navigation.PushAsync(new CourseDisplayPage(new CourseDisplayViewModel(term)));

            TermsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Terms.Count == 0)
                viewModel.LoadTermsCommand.Execute(null);
        }
    }
}