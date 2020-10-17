using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace C971.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int CourseID { get; set; }
        public int TermID { get; set; }
        public string CourseTitle { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public CourseStatuses CourseStatus { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string InstructorPhone { get; set; }
        public string CourseNotes { get; set; }
    }

    public enum CourseStatuses
    {
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Completed")]
        Completed,
        [Display(Name = "Dropped")]
        Dropped,
        [Display(Name = "Plan To Take")]
        PlanToTake
    }
}
