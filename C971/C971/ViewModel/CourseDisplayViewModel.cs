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
    public class CourseDisplayViewModel : BaseViewModel
    {
        public ObservableCollection<Course> Courses { get; set; }
        public Term Term { get; set; }
        public Command LoadCoursesCommand { get; set; }

        public CourseDisplayViewModel(Term term)
        {
            Term = term;
            Courses = new ObservableCollection<Course>();
            LoadCoursesCommand = new Command(async () => await ExecuteLoadCoursesCommand());

            MessagingCenter.Subscribe<AddModifyCoursePage, Course>(this, "SaveCourse",
                async (sender, course) =>
                {
                    Courses.Add(course);
                    await DataStore.AddCourseAsync(course);
                });

            MessagingCenter.Subscribe<AddModifyCoursePage, Course>(this, "UpdateCourse",
                async (sender, course) =>
                {
                    await DataStore.UpdateCourseAsync(course, course.CourseID);
                    await ExecuteLoadCoursesCommand();
                });
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