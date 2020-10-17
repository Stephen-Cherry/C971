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
            if (CourseStartDate.Date > CourseEndDate.Date)
            {
                await DisplayAlert("Error", "The start date must be before the end date.", "Ok");
                return;
            }
            var input = await DisplayAlert("Save", "Save Course?", "Yes", "No");
            if (input == true)
            {
                var message = viewModel.IsNewCourse ? "SaveCourse" : "UpdateCourse";
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