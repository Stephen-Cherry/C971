using C971.Models;
using C971.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace C971.ViewModel
{
    public class AssessmentDisplayViewModel : BaseViewModel
    {
        public Assessment Assessment { get; set; }

        public AssessmentDisplayViewModel(Assessment assessment)
        {
            Assessment = assessment;

            MessagingCenter.Subscribe<AddModifyAssessmentPage, Term>(this, "UpdateAssesment",
                 (sender, updateAssessment) =>
                 {
                 });
        }
    }
}