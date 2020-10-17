using C971.Models;
using C971.ViewModel;
using System;

using Xamarin.Forms;

namespace C971.Views
{
    public partial class CourseDisplayPage : ContentPage
    {
        private CourseDisplayViewModel viewModel { get; set; }

        public CourseDisplayPage(CourseDisplayViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddModifyCoursePage()));
        }

        private async void Edit_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddModifyTermPage(new AddModifyTermViewModel(viewModel.Term))));
        }

        private async void Delete_Clicked(object sender, EventArgs args)
        {
            var input = await DisplayAlert("Delete", "Delete Term?\n\nWARNING: THIS WILL DELETE COURSES AND ASSESSMENTS ASSOCIATED WITH THIS TERM!", "Yes", "No");
            if (input == true)
            {
                var message = "DeleteTerm";
                MessagingCenter.Send(this, message, viewModel.Term);
                await Navigation.PopAsync();
            }
        }

        private async void Course_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentDisplayPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Courses.Count == 0)
                viewModel.LoadCoursesCommand.Execute(null);
        }
    }
}