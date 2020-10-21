using C971.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    public partial class AddModifyCoursePage : ContentPage
    {
        AddModifyCourseViewModel viewModel;

        public AddModifyCoursePage(AddModifyCourseViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }
        public AddModifyCoursePage()
        {
            InitializeComponent();

            viewModel = new AddModifyCourseViewModel();
            BindingContext = viewModel;
            viewModel.CourseStartDate = DateTime.Now;
            viewModel.CourseEndDate = DateTime.Now;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (CourseTitle.Text == null)
            {
                await DisplayAlert("Error", "New course must include a title.", "Ok");
                return;
            }
            else if (InstructorName.Text == null)
            {
                await DisplayAlert("Error", "New course must include an instructor name.", "Ok");
                return;
            }
            else if(InstructorEmail.Text == null)
            {
                await DisplayAlert("Error", "New course must include an instructor email.", "Ok");
                return;
            }
            else if(InstructorPhone.Text == null)
            {
                await DisplayAlert("Error", "New course must include an instructor phone.", "Ok");
                return;
            }
            else if(CourseStartDate.Date > CourseEndDate.Date)
            {
                await DisplayAlert("Error", "The start date must be before the end date.", "Ok");
                return;
            }
            else if (!InstructorEmail.Text.Contains("@") || !InstructorEmail.Text.Contains("."))
            {
                await DisplayAlert("Error", "Email address must contain a \"@\" and a \".\".", "Ok");
                return;
            }
            var input = await DisplayAlert("Save", "Save Course?", "Yes", "No");
            if (input == true)
            {
                var message = viewModel.IsNewCourse ? "SaveCourse" : "UpdateCourse";
                viewModel.Course.CourseStatus = CourseStatus.SelectedItem.ToString().ToEnum();
                MessagingCenter.Send(this, message, viewModel.Course);
                await Navigation.PopModalAsync();
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            var input = await DisplayAlert("Cancel", "Cancel Course?", "Yes", "No");
            if (input == true)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}