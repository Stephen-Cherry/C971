using C971.Models;
using System;

namespace C971.ViewModel
{
    public class AddModifyAssessmentViewModel : BaseViewModel
    {
        public Assessment Assessment { get; set; }

        public bool IsNewAssessment { get; set; }

        public String AssessmentTitle
        {
            get { return Assessment.AssessmentTitle; }
            set
            {
                Assessment.AssessmentTitle = value;
                OnPropertyChanged();
            }
        }

        public DateTime AssessmentStartDate
        {
            get { return Assessment.AssessmentStartDate; }
            set
            {
                Assessment.AssessmentStartDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime AssessmentEndDate
        {
            get { return Assessment.AssessmentEndDate; }
            set
            {
                Assessment.AssessmentEndDate = value;
                OnPropertyChanged();
            }
        }
        public AssessmentType AssessmentType
        {
            get { return Assessment.AssessmentType; }
            set
            {
                Assessment.AssessmentType = value;
                OnPropertyChanged();
            }
        }

        public string AssessmentNotes
        {
            get { return Assessment.AssessmentNotes; }
            set
            {
                Assessment.AssessmentNotes = value;
                OnPropertyChanged();
            }
        }

        public AddModifyAssessmentViewModel(Assessment assessment = null)
        {
            IsNewAssessment = assessment == null;

            Title = IsNewAssessment ? "Add Assessment" : "Edit Assessment";
            Assessment = assessment ?? new Assessment();
        }
    }
}