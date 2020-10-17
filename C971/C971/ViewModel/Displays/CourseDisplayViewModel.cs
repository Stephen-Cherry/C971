using C971.Models;
using C971.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace C971.ViewModel
{
    public class CourseDisplayViewModel : BaseViewModel
    {
        public ObservableCollection<Assessment> Assessments { get; set; }
        public Course Course { get; set; }
        public Command LoadAssessmentsCommand { get; set; }

        public CourseDisplayViewModel(Course course)
        {
            Course = course;
            Title = course.CourseTitle;
            CourseStartDate = course.CourseStartDate.ToString("dd-MMM-yyyy");
            CourseEndDate = course.CourseEndDate.ToString("dd-MMM-yyyy");
            Assessments = new ObservableCollection<Assessment>();
            LoadAssessmentsCommand = new Command(async () => await ExecuteLoadAssessmentsCommand());

            MessagingCenter.Subscribe<AddModifyAssessmentPage, Assessment>(this, "SaveAssessment",
                async (sender, assessment) =>
                {
                    assessment.CourseID = course.CourseID;
                    Assessments.Add(assessment);
                    await DataStore.AddAssessmentAsync(assessment);
                });

            MessagingCenter.Subscribe<AddModifyAssessmentPage, Assessment>(this, "UpdateAssessment",
                async (sender, assessment) =>
                {
                    assessment.CourseID = course.CourseID;
                    await DataStore.UpdateAssessmentAsync(assessment, assessment.CourseID);
                    await ExecuteLoadAssessmentsCommand();
                });

            MessagingCenter.Subscribe<AddModifyCoursePage, Term>(this, "UpdateCourse",
                 (sender, updateTerm) =>
                 {
                     Title = course.CourseTitle;
                     CourseStartDate = course.CourseStartDate.ToString("dd-MMM-yyyy");
                     CourseEndDate = course.CourseEndDate.ToString("dd-MMM-yyyy");
                 });

            MessagingCenter.Subscribe<TermDisplayPage, Assessment>(this, "DeleteAssessment",
                async (sender, assessment) =>
                {
                    await DataStore.DeleteAssessmentAsync(assessment);
                    await ExecuteLoadAssessmentsCommand();
                });
        }
        string coursestartdate = string.Empty;
        public string CourseStartDate
        {
            get { return CourseStartDate; }
            set { SetProperty(ref coursestartdate, value); }
        }

        string courseenddate = string.Empty;
        public string CourseEndDate
        {
            get { return CourseEndDate; }
            set { SetProperty(ref courseenddate, value); }
        }
        private async Task ExecuteLoadAssessmentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Assessments.Clear();
                IList<Assessment> assessments = await DataStore.GetAssessmentsAsync();
                foreach (Assessment assessment in assessments)
                {
                    if (assessment.CourseID == Course.CourseID)
                    {
                        Assessments.Add(assessment);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
