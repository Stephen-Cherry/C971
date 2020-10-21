using SQLite;
using System;
using System.Collections.Generic;
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
        InProgress,
        Completed,
        Dropped,
        PlanToTake
    }

    public static class CourseStatusesExtensions
    {
        public static string ToFriendlyString(this CourseStatuses status)
        {
            switch (status)
            {
                case CourseStatuses.InProgress:
                    return "In Progress";
                case CourseStatuses.Completed:
                    return "Completed";
                case CourseStatuses.Dropped:
                    return "Dropped";
                case CourseStatuses.PlanToTake:
                    return "Plan To Take";
                default:
                    return "";
            }
        }

        public static CourseStatuses ToEnum(this string status)
        {
            switch (status)
            {
                case "In Progress":
                    return CourseStatuses.InProgress;
                case "Completed":
                    return CourseStatuses.Completed;
                case "Dropped":
                    return CourseStatuses.Dropped;
                case "Plan To Take":
                    return CourseStatuses.PlanToTake;
                default:
                    return CourseStatuses.PlanToTake;
            }
        }
    }
}
