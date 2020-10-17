using C971.Models;
using System;
using System.Collections.Generic;

namespace C971.ViewModel
{
    public class AddModifyCourseViewModel : BaseViewModel
    {
        public Course Course { get; set; }

        public IList<CourseStatuses> CourseStatusList { get; set; }

        public bool IsNewCourse { get; set; }

        public String CourseTitle
        {
            get { return Course.CourseTitle; }
            set
            {
                Course.CourseTitle = value;
                OnPropertyChanged();
            }
        }

        public DateTime CourseStartDate
        {
            get { return Course.CourseStartDate; }
            set
            {
                Course.CourseStartDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime CourseEndDate
        {
            get { return Course.CourseEndDate; }
            set
            {
                Course.CourseEndDate = value;
                OnPropertyChanged();
            }
        }

        public CourseStatuses CourseStatus
        {
            get { return Course.CourseStatus; }
            set
            {
                Course.CourseStatus = value;
                OnPropertyChanged();
            }
        }

        public string InstructorName
        {
            get { return Course.InstructorName; }
            set
            {
                Course.InstructorName = value;
                OnPropertyChanged();
            }
        }

        public string InstructorPhone
        {
            get { return Course.InstructorPhone; }
            set
            {
                Course.InstructorPhone = value;
                OnPropertyChanged();
            }
        }

        public string InstructorEmail
        {
            get { return Course.InstructorEmail; }
            set
            {
                Course.InstructorEmail = value;
                OnPropertyChanged();
            }
        }

        public string CourseNotes
        {
            get { return Course.CourseNotes; }
            set
            {
                Course.CourseNotes = value;
                OnPropertyChanged();
            }
        }

        async void InitializeCourseStatusList()
        {
            CourseStatusList = await DataStore.GetCourseStatusesAsync();
        }

        public AddModifyCourseViewModel(Course course = null)
        {
            IsNewCourse = course == null;
            InitializeCourseStatusList();
            Title = IsNewCourse ? "Add Course" : "Edit Course";
            Course = course ?? new Course();
        }
    }
}