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
    public partial class AssessmentDisplayPage : ContentPage
    {
        private AssessmentDisplayViewModel viewModel { get; set; }

        public AssessmentDisplayPage(AssessmentDisplayViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        private async void Edit_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddModifyAssessmentPage(new AddModifyAssessmentViewModel(viewModel.Assessment.CourseID, viewModel.Assessment))));
        }

        private async void Delete_Clicked(object sender, EventArgs args)
        {
            var input = await DisplayAlert("Delete", "Delete Assessment?", "Yes", "No");
            if (input == true)
            {
                var message = "DeleteAssessment";
                MessagingCenter.Send(this, message, viewModel.Assessment);
                await Navigation.PopAsync();
            }
        }
    }
}