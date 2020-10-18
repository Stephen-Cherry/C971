using C971.Models;
using C971.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    public partial class CourseDisplayPage : ContentPage
    {
        private CourseDisplayViewModel viewModel { get; set; }

        public CourseDisplayPage(CourseDisplayViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }


        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Assessments.Count < 2)
            {
                await Navigation.PushModalAsync(new NavigationPage(new AddModifyAssessmentPage(viewModel.Course.CourseID)));
            }
            else
            {
                await DisplayAlert("Course Limit Reached", $"{viewModel.CourseTitle} has reached the Assessment limit of 2.", "Ok");
            }
            
        }

        private async void Edit_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddModifyCoursePage(new ViewModel.AddModifyCourseViewModel(viewModel.Course))));
        }

        private async void Delete_Clicked(object sender, EventArgs args)
        {
            var input = await DisplayAlert("Delete", "Delete Course?\n\nWARNING: THIS WILL DELETE ASSESSMENTS ASSOCIATED WITH THIS COURSE!", "Yes", "No");
            if (input == true)
            {
                var message = "DeleteCourse";
                MessagingCenter.Send(this, message, viewModel.Course);
                await Navigation.PopAsync();
            }
        }

        private async void Assessment_Clicked(object sender, SelectedItemChangedEventArgs args)
        {
            Assessment assessment = args.SelectedItem as Assessment;
            if (assessment == null)
            { return; }
            await Navigation.PushAsync(new AssessmentDisplayPage(new AssessmentDisplayViewModel(assessment)));
            AssessmentsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Assessments.Count == 0)
                viewModel.LoadAssessmentsCommand.Execute(null);
        }
    }
}