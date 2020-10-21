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
            PopulateCourseView(course);
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
                    await DataStore.UpdateAssessmentAsync(assessment);
                    await ExecuteLoadAssessmentsCommand();
                });

            MessagingCenter.Subscribe<AddModifyCoursePage, Course>(this, "UpdateCourse",
                 (sender, updatedcourse) =>
                 {
                     PopulateCourseView(updatedcourse);

                 });

            MessagingCenter.Subscribe<AssessmentDisplayPage, Assessment>(this, "DeleteAssessment",
                async (sender, assessment) =>
                {
                    await DataStore.DeleteAssessmentAsync(assessment);
                    await ExecuteLoadAssessmentsCommand();
                });
        }

        private void PopulateCourseView(Course course)
        {
            CourseTitle = course.CourseTitle;
            CourseStartDate = course.CourseStartDate.ToString("dd-MMM-yyyy");
            CourseEndDate = course.CourseEndDate.ToString("dd-MMM-yyyy");
            CourseStatus = course.CourseStatus.ToFriendlyString();
            InstructorName = course.InstructorName;
            InstructorEmail = course.InstructorEmail;
            InstructorPhone = course.InstructorPhone;
            CourseNotes = course.CourseNotes;
        }

        string coursetitle = string.Empty;
        public string CourseTitle
        {
            get { return coursetitle; }
            set { SetProperty(ref coursetitle, value); }
        }

        string coursestartdate = string.Empty;
        public string CourseStartDate
        {
            get { return coursestartdate; }
            set { SetProperty(ref coursestartdate, value); }
        }

        string courseenddate = string.Empty;
        public string CourseEndDate
        {
            get { return courseenddate; }
            set { SetProperty(ref courseenddate, value); }
        }

        string coursestatus = string.Empty;
        public string CourseStatus
        {
            get { return coursestatus; }
            set { SetProperty(ref coursestatus, value); }
        }

        string instructorname = string.Empty;
        public string InstructorName
        {
            get { return instructorname; }
            set { SetProperty(ref instructorname, value); }
        }

        string instructoremail = string.Empty;
        public string InstructorEmail
        {
            get { return instructoremail; }
            set { SetProperty(ref instructoremail, value); }
        }

        string instructorphone = string.Empty;
        public string InstructorPhone
        {
            get { return instructorphone; }
            set { SetProperty(ref instructorphone, value); }
        }

        string coursenotes = string.Empty;
        public string CourseNotes
        {
            get { return coursenotes; }
            set { SetProperty(ref coursenotes, value); }
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
