using C971.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.ViewModel
{
    public class AddModifyAssessmentViewModel : BaseViewModel
    {
        public Course ParentCourse { get; set; }
        public bool ParentHasObjective { get; set; }
        public bool ParentHasPerformance { get; set; }
        public Assessment Assessment { get; set; }
        public IList<AssessmentType> AssessmentTypeList { get; set; }
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

        public DateTime AssessmentDueDate
        {
            get { return Assessment.AssessmentDueDate; }
            set
            {
                Assessment.AssessmentDueDate = value;
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

        private async void InitializeAssessmentTypeList()
        {
            AssessmentTypeList = await DataStore.GetAssessmentTypesAsync();
        }

        private async void InitializeParentCourse()
        {
            ParentCourse = await DataStore.GetCourseAsync(Assessment.CourseID);
        }

        private async void ParentAssessmentTypesCheck()
        {
            IList<Assessment> assessments = await DataStore.GetAssessmentsAsync();
            foreach (Assessment assessment in assessments)
            {
                if (assessment.CourseID == ParentCourse.CourseID)
                {
                    if (assessment.AssessmentType == AssessmentType.Objective)
                    {
                        ParentHasObjective = true;
                    }
                    else if (assessment.AssessmentType == AssessmentType.Performance)
                    {
                        ParentHasPerformance = true;
                    }
                }
            }
        }

        public AddModifyAssessmentViewModel(int courseID, Assessment assessment = null)
        {
            ParentHasPerformance = false;
            ParentHasObjective = false;
            IsNewAssessment = assessment == null;
            InitializeAssessmentTypeList();
            Title = IsNewAssessment ? "Add Assessment" : "Edit Assessment";
            Assessment = assessment ?? new Assessment() { CourseID = courseID };
            InitializeParentCourse();
            ParentAssessmentTypesCheck();
        }
    }
}