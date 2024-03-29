﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971.Models
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int AssessmentID { get; set; }
        public int CourseID { get; set; }
        public string AssessmentTitle { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public DateTime AssessmentDueDate { get; set; }
    }

    public enum AssessmentType
    {
        Objective,
        Performance
    }
}
