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
            PopulateAssessmentView(assessment);

            MessagingCenter.Subscribe<AddModifyAssessmentPage, Assessment>(this, "UpdateAssesment",
                 (sender, updatedassessment) =>
                 {
                     PopulateAssessmentView(updatedassessment);
                 });
        }

        private void PopulateAssessmentView(Assessment assessment)
        {
            AssessmentTitle = assessment.AssessmentTitle;
            AssessmentStartDate = assessment.AssessmentStartDate.ToString("dd-MMM-yyyy");
            AssessmentEndDate = assessment.AssessmentEndDate.ToString("dd-MMM-yyyy");
            AssessmentNotes = assessment.AssessmentNotes;
        }

        string assessmenttitle = string.Empty;
        public string AssessmentTitle
        {
            get { return assessmenttitle; }
            set { SetProperty(ref assessmenttitle, value); }
        }

        string assessmentstartdate = string.Empty;
        public string AssessmentStartDate
        {
            get { return assessmentstartdate; }
            set { SetProperty(ref assessmentstartdate, value); }
        }

        string assessmentenddate = string.Empty;
        public string AssessmentEndDate
        {
            get { return assessmentenddate; }
            set { SetProperty(ref assessmentenddate, value); }
        }

        string assessmentnotes = string.Empty;
        public string AssessmentNotes
        {
            get { return assessmentnotes; }
            set { SetProperty(ref assessmentnotes, value); }
        }
    }
}