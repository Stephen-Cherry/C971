using C971.Models;
using C971.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    public partial class AddModifyAssessmentPage : ContentPage
    {
        AddModifyAssessmentViewModel viewModel;

        public AddModifyAssessmentPage(AddModifyAssessmentViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        public AddModifyAssessmentPage()
        {
            InitializeComponent();

            viewModel = new AddModifyAssessmentViewModel();
            BindingContext = viewModel;
            viewModel.AssessmentStartDate = DateTime.Now;
            viewModel.AssessmentEndDate = DateTime.Now;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (AssessmentTitle.Text == null)
            {
                await DisplayAlert("Error", "New assessment must include a title.", "Ok");
                return;
            }
            if (AssessmentStartDate.Date > AssessmentEndDate.Date)
            {
                await DisplayAlert("Error", "The start date must be before the end date.", "Ok");
                return;
            }
            var input = await DisplayAlert("Save", "Save Term?", "Yes", "No");
            if (input == true)
            {
                var message = viewModel.IsNewAssessment ? "SaveAssessment" : "UpdateAssessment";
                MessagingCenter.Send(this, message, viewModel.Assessment);
                await Navigation.PopModalAsync();
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            var input = await DisplayAlert("Cancel", "Cancel Assessment?", "Yes", "No");
            if (input == true)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}