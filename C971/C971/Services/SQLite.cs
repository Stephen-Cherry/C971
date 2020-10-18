﻿using C971.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace C971.Services
{
    public static class DBPath
    {
        public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "C971.db3");
    }

    public class SQLiteDataStore : IDataStore
    {
        public SQLiteConnection DB { get; set; }

        public static bool FirstLaunch { get; set; }

        public SQLiteDataStore()
        {
            DB = new SQLiteConnection(DBPath.dbPath);
            MockDataCheck();
        }

        private void MockDataCheck()
        {
            if (FirstLaunch == true)
            {
                if (DB.Table<Term>().Count() == 0)
                {
                    DB.DropTable<Term>();
                    DB.DropTable<Course>();
                    DB.DropTable<Assessment>();
                    DB.CreateTable<Term>();
                    DB.CreateTable<Course>();
                    DB.CreateTable<Assessment>();

                    DB.Insert(new Term() {TermTitle = "Term 1", TermStartDate = new DateTime(2021, 1, 1), TermEndDate = new DateTime(2021, 12, 31) });

                    DB.Insert(new Course() {CourseTitle = "Course 1", TermID = 1, CourseStartDate = new DateTime(2021, 1, 1), CourseEndDate = new DateTime(2021, 1, 31), CourseStatus = CourseStatuses.InProgress, InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", CourseNotes = "N/A" });

                    DB.Insert(new Assessment() { CourseID = 1, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentDueDate = new DateTime(2021, 1, 1) });
                    DB.Insert(new Assessment() { CourseID = 1, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentDueDate = new DateTime(2021, 2, 1) });
                }
            }
            FirstLaunch = false;
        }

        public async Task<int> AddAssessmentAsync(Assessment assessment)
        {
            DB.Insert(assessment);
            return await Task.FromResult(assessment.AssessmentID);
        }

        public async Task<int> AddCourseAsync(Course course)
        {
            DB.Insert(course);
            return await Task.FromResult(course.CourseID);
        }

        public async Task<int> AddTermAsync(Term term)
        {
            DB.Insert(term);
            return await Task.FromResult(term.TermID);
        }

        public async Task<Assessment> GetAssessmentAsync(int id)
        {
            Assessment assessment = DB.Table<Assessment>().FirstOrDefault(a => a.AssessmentID == id);

            return await Task.FromResult(assessment);
        }

        public async Task<IList<Assessment>> GetAssessmentsAsync()
        {
            TableQuery<Assessment> dbAssessments = DB.Table<Assessment>();
            List<Assessment> AssessmentList = new List<Assessment>();
            foreach (Assessment assessment in dbAssessments)
            {
                AssessmentList.Add(assessment);
            }
            return await Task.FromResult(AssessmentList);
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            Course course = DB.Table<Course>().FirstOrDefault(c => c.CourseID == id);

            return await Task.FromResult(course);
        }

        public async Task<IList<Course>> GetCoursesAsync()
        {
            TableQuery<Course> dbCourses = DB.Table<Course>();
            List<Course> CourseList = new List<Course>();
            foreach (Course course in dbCourses)
            {
                CourseList.Add(course);
            }
            return await Task.FromResult(CourseList);
        }

        public async Task<Term> GetTermAsync(int id)
        {
            Term term = DB.Table<Term>().FirstOrDefault(t => t.TermID == id);

            return await Task.FromResult(term);
        }

        public async Task<IList<Term>> GetTermsAsync()
        {
            TableQuery<Term> dbTerms = DB.Table<Term>();
            List<Term> TermList = new List<Term>();
            foreach (Term term in dbTerms)
            {
                TermList.Add(term);
            }
            return await Task.FromResult(TermList);
        }

        public async Task<bool> UpdateAssessmentAsync(Assessment assessment)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath.dbPath);
            Assessment tableAssessment = await GetAssessmentAsync(assessment.AssessmentID);
            bool assessmentFound = tableAssessment != null;
            if (assessmentFound)
            {
                tableAssessment.AssessmentTitle = assessment.AssessmentTitle;
                tableAssessment.AssessmentDueDate = assessment.AssessmentDueDate;
                db.Update(tableAssessment);
            }
            return await Task.FromResult(assessmentFound);
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath.dbPath);
            Course CourseTable = await GetCourseAsync(course.CourseID);
            bool courseFound = CourseTable != null;
            if (courseFound)
            {
                CourseTable.CourseTitle = course.CourseTitle;
                CourseTable.CourseStartDate = course.CourseStartDate;
                CourseTable.CourseEndDate = course.CourseEndDate;
                db.Update(CourseTable);
            }
            return await Task.FromResult(courseFound);
        }

        public async Task<bool> UpdateTermAsync(Term term)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath.dbPath);
            Term tableTerm = await GetTermAsync(term.TermID);
            bool termFound = tableTerm != null;
            if (termFound)
            {
                tableTerm.TermTitle = term.TermTitle;
                tableTerm.TermStartDate = term.TermStartDate;
                tableTerm.TermEndDate = term.TermEndDate;
                db.Update(tableTerm);
            }
            return await Task.FromResult(termFound);
        }

        public async Task<bool> DeleteTermAsync(Term term)
        {
            bool IsSuccess = false;
            DB.Delete(term);

            foreach (Course course in DB.Table<Course>())
            {
                if (course.TermID == term.TermID)
                {
                    DB.Delete(course);
                    foreach (Assessment assessment in DB.Table<Assessment>())
                    {
                        if (assessment.CourseID == course.CourseID)
                        {
                            DB.Delete(assessment);
                        }
                    }
                }
            }
            IsSuccess = true;

            return await Task.FromResult(IsSuccess);
        }

        public async Task<bool> DeleteCourseAsync(Course course)
        {
            bool IsSuccess = false;
            DB.Delete(course);
            foreach (Assessment assessment in DB.Table<Assessment>())
            {
                if (assessment.CourseID == course.CourseID)
                {
                    DB.Delete(assessment);
                }
            }
            IsSuccess = true;

            return await Task.FromResult(IsSuccess);
        }

        public async Task<bool> DeleteAssessmentAsync(Assessment assessment)
        {
            bool IsSuccess = false;
            DB.Delete(assessment);
            IsSuccess = true;
            return await Task.FromResult(IsSuccess);
        }

        public async Task<IList<CourseStatuses>> GetCourseStatusesAsync()
        {
            List<CourseStatuses> CourseStatusList = new List<CourseStatuses>();

            foreach (CourseStatuses coursestatus in (CourseStatuses[])Enum.GetValues(typeof(CourseStatuses)))
            {
                CourseStatusList.Add(coursestatus);
            }
            return await Task.FromResult(CourseStatusList);
        }

        public async Task<IList<AssessmentType>> GetAssessmentTypesAsync()
        {
            List<AssessmentType> AssessmentTypeList = new List<AssessmentType>();

            foreach (AssessmentType coursestatus in (AssessmentType[])Enum.GetValues(typeof(AssessmentType)))
            {
                AssessmentTypeList.Add(coursestatus);
            }
            return await Task.FromResult(AssessmentTypeList);
        }
    }
}