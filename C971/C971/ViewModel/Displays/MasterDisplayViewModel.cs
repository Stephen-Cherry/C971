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
    internal class MasterDisplayViewModel : BaseViewModel
    {
        public ObservableCollection<Term> Terms { get; set; }
        public static bool FirstLaunch { get; set; }
        public static bool HasNotifications { get; set; }
        public Command LoadTermsCommand { get; set; }

        public MasterDisplayViewModel()
        {
            Title = "Terms";
            Terms = new ObservableCollection<Term>();
            LoadTermsCommand = new Command(async () => await ExecuteLoadTermsCommand());

            MessagingCenter.Subscribe<AddModifyTermPage, Term>(this, "SaveTerm",
    async (sender, term) =>
    {
        Terms.Add(term);
        await DataStore.AddTermAsync(term);
    });

            MessagingCenter.Subscribe<AddModifyTermPage, Term>(this, "UpdateTerm",
                async (sender, term) =>
                {
                    await DataStore.UpdateTermAsync(term);
                    await ExecuteLoadTermsCommand();
                });

            MessagingCenter.Subscribe<TermDisplayPage, Term>(this, "DeleteTerm",
    async (sender, term) =>
    {
        await DataStore.DeleteTermAsync(term);
        await ExecuteLoadTermsCommand();
    });
        }

        private async Task ExecuteLoadTermsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Terms.Clear();
                IList<Term> terms = await DataStore.GetTermsAsync();
                foreach (Term term in terms)
                {
                    Terms.Add(term);
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

        public async Task<string> DateNotifications()
        {
            IList<Term> Terms = await DataStore.GetTermsAsync();
            IList<Course> Courses = await DataStore.GetCoursesAsync();
            IList<Assessment> Assessments = await DataStore.GetAssessmentsAsync();
            HasNotifications = false;
            string notificationString;
            string termStart = "";
            string termEnd = "";
            string courseStart = "";
            string courseEnd = "";
            string assessmentDue = "";

            foreach (Term term in Terms)
            {
                if (term.TermStartDate == DateTime.Today)
                {
                    termStart += $"{term.TermTitle}\n";
                    HasNotifications = true;
                }
                if (term.TermEndDate == DateTime.Today)
                {
                    termEnd += $"{term.TermTitle}\n";
                    HasNotifications = true;
                }
            }

            foreach (Course course in Courses)
            {
                if (course.CourseStartDate == DateTime.Today)
                {
                    courseStart += $"{course.CourseTitle}\n";
                    HasNotifications = true;
                }
                if (course.CourseEndDate == DateTime.Today)
                {
                    courseEnd += $"{course.CourseTitle}\n";
                    HasNotifications = true;
                }
            }

            foreach (Assessment assessment in Assessments)
            {
                if (assessment.AssessmentDueDate == DateTime.Today)
                {
                    assessmentDue += $"{assessment.AssessmentTitle}\n";
                    HasNotifications = true;
                }
            }

            notificationString = $"The following Term(s) start today:\n\n" +
                $"{termStart}\n\n" +
                $"The following Term(s) end today:\n\n" +
                $"{termEnd}\n\n" +
                $"The following Course(s) start today:\n\n" +
                $"{courseStart}\n\n" +
                $"The following Course(s) end today:\n\n" +
                $"{courseEnd}\n\n" +
                $"The following Assessment(s) are due today:\n\n" +
                $"{assessmentDue}";

            return notificationString;
        }
    }
}