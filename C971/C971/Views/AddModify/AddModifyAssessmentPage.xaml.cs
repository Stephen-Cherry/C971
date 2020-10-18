using C971.Models;
using C971.Services;
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

        public AddModifyAssessmentPage(int courseID)
        {
            InitializeComponent();

            viewModel = new AddModifyAssessmentViewModel(courseID);
            BindingContext = viewModel;
            viewModel.AssessmentDueDate = DateTime.Now;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (AssessmentTitle.Text == null)
            {
                await DisplayAlert("Error", "New assessment must include a title.", "Ok");
                return;
            }
            else if (AssessmentDueDate.Date > viewModel.ParentCourse.CourseStartDate || AssessmentDueDate.Date < viewModel.ParentCourse.CourseEndDate)
            {
                await DisplayAlert("Error", $"Assessment due date must be between parent course dates.\n\n{viewModel.ParentCourse.CourseTitle}\nStart Date: {viewModel.ParentCourse.CourseStartDate:dd-MMM-yyyy}\nEnd Date: {viewModel.ParentCourse.CourseEndDate:dd-MMM-yyyy}", "Ok");
                return;
            }
            else if ((AssessmentType.SelectedItem.ToString() == "Objective" && viewModel.ParentHasObjective == true) || (AssessmentType.SelectedItem.ToString() == "Performance" && viewModel.ParentHasPerformance == true))
            {
                await DisplayAlert("Error", $"{viewModel.ParentCourse.CourseTitle} already has an assessment with the type of {AssessmentType.SelectedItem}.  A course may only have one of each assessment type.", "Ok");
                return;
            }
            var input = await DisplayAlert("Save", "Save Assessment?", "Yes", "No");
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