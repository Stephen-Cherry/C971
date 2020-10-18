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

            MessagingCenter.Subscribe<AddModifyAssessmentPage, Assessment>(this, "UpdateAssessment",
                 (sender, updatedassessment) =>
                 {
                     PopulateAssessmentView(updatedassessment);
                 });
        }

        private void PopulateAssessmentView(Assessment assessment)
        {
            AssessmentTitle = assessment.AssessmentTitle;
            AssessmentDueDate = assessment.AssessmentDueDate.ToString("dd-MMM-yyyy");
            AssessmentType = assessment.AssessmentType.ToString("");
            AssessmentNotes = assessment.AssessmentNotes;
        }

        string assessmenttitle = string.Empty;
        public string AssessmentTitle
        {
            get { return assessmenttitle; }
            set { SetProperty(ref assessmenttitle, value); }
        }

        string assessmentduedate = string.Empty;
        public string AssessmentDueDate
        {
            get { return assessmentduedate; }
            set { SetProperty(ref assessmentduedate, value); }
        }

        string assessmenttype = string.Empty;
        public string AssessmentType
        {
            get { return assessmenttype; }
            set { SetProperty(ref assessmenttype, value); }
        }

        string assessmentnotes = string.Empty;
        public string AssessmentNotes
        {
            get { return assessmentnotes; }
            set { SetProperty(ref assessmentnotes, value); }
        }
    }
}