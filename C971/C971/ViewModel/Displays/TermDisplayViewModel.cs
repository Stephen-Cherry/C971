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
    public class TermDisplayViewModel : BaseViewModel
    {
        public ObservableCollection<Course> Courses { get; set; }
        public Term Term { get; set; }
        public Command LoadCoursesCommand { get; set; }

        public TermDisplayViewModel(Term term)
        {
            Term = term;
            PopulateTermView(term);
            Courses = new ObservableCollection<Course>();
            LoadCoursesCommand = new Command(async () => await ExecuteLoadCoursesCommand());

            MessagingCenter.Subscribe<AddModifyCoursePage, Course>(this, "SaveCourse",
                async (sender, course) =>
                {
                    course.TermID = term.TermID;
                    Courses.Add(course);
                    await DataStore.AddCourseAsync(course);
                });

            MessagingCenter.Subscribe<AddModifyCoursePage, Course>(this, "UpdateCourse",
                async (sender, course) =>
                {
                    course.TermID = term.TermID;
                    await DataStore.UpdateCourseAsync(course);
                    await ExecuteLoadCoursesCommand();
                });

            MessagingCenter.Subscribe<AddModifyTermPage, Term>(this, "UpdateTerm",
                 (sender, updatedTerm) =>
                 {
                     PopulateTermView(updatedTerm);
                 });

            MessagingCenter.Subscribe<CourseDisplayPage, Course>(this, "DeleteCourse",
            async (sender, course) =>
            {
                await DataStore.DeleteCourseAsync(course);
                await ExecuteLoadCoursesCommand();
            });
        }

        private void PopulateTermView(Term term)
        {
            TermTitle = term.TermTitle;
            TermStartDate = term.TermStartDate.ToString("dd-MMM-yyyy");
            TermEndDate = term.TermEndDate.ToString("dd-MMM-yyyy");
        }

        private string termtitle = string.Empty;

        public string TermTitle
        {
            get { return termtitle; }
            set { SetProperty(ref termtitle, value); }
        }

        private string termstartdate = string.Empty;

        public string TermStartDate
        {
            get { return termstartdate; }
            set { SetProperty(ref termstartdate, value); }
        }

        private string termenddate = string.Empty;

        public string TermEndDate
        {
            get { return termenddate; }
            set { SetProperty(ref termenddate, value); }
        }

        private async Task ExecuteLoadCoursesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Courses.Clear();
                IList<Course> courses = await DataStore.GetCoursesAsync();
                foreach (Course course in courses)
                {
                    if (course.TermID == Term.TermID)
                    {
                        Courses.Add(course);
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