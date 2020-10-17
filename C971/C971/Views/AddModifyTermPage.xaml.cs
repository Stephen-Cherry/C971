using C971.Models;
using C971.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    public partial class AddModifyTermPage : ContentPage
    {
        AddModifyTermViewModel viewModel;

        public AddModifyTermPage(AddModifyTermViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        public AddModifyTermPage()
        {
            InitializeComponent();

            viewModel = new AddModifyTermViewModel();
            BindingContext = viewModel;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var input = await DisplayAlert("Save", "Save Term?", "Yes", "No");
            if (input == true)
            {
                var message = viewModel.IsNewTerm ? "SaveTerm" : "UpdateTerm";
                MessagingCenter.Send(this, message, viewModel.Term);
                await Navigation.PopModalAsync();
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            var input = await DisplayAlert("Cancel", "Cancel Term?", "Yes", "No");
            if (input == true)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}