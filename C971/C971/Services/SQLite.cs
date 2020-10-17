using C971.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public SQLiteDataStore()
        {
            DB = new SQLiteConnection(DBPath.dbPath);
            DB.CreateTable<Term>();
            DB.CreateTable<Course>();
            DB.CreateTable<Assessment>();
            MockDataCheck(DB);

        }

        private void MockDataCheck(SQLiteConnection db)
        {
            if (db.Table<Term>().Count() == 0)
            {
                db.Insert(new Term() { TermID=1, TermTitle = "Term 1", TermStartDate = new DateTime(2021, 1, 1), TermEndDate = new DateTime(2021, 12, 31) });

                db.Insert(new Course() { CourseID = 1, CourseTitle = "Course 1", TermID = 1, CourseStartDate = new DateTime(2021, 1, 1), CourseEndDate = new DateTime(2021, 1, 31), CourseStatus = "Started", InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", Notes = "N/A" });
                db.Insert(new Course() { CourseID = 2, CourseTitle = "Course 2", TermID = 1, CourseStartDate = new DateTime(2021, 2, 1), CourseEndDate = new DateTime(2021, 1, 28), CourseStatus = "Pending", InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", Notes = "N/A" });
                db.Insert(new Course() { CourseID = 3, CourseTitle = "Course 3", TermID = 1, CourseStartDate = new DateTime(2021, 1, 1), CourseEndDate = new DateTime(2021, 1, 31), CourseStatus = "Started", InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", Notes = "N/A" });
                db.Insert(new Course() { CourseID = 4, CourseTitle = "Course 4", TermID = 1, CourseStartDate = new DateTime(2021, 2, 1), CourseEndDate = new DateTime(2021, 1, 28), CourseStatus = "Pending", InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", Notes = "N/A" });
                db.Insert(new Course() { CourseID = 5, CourseTitle = "Course 5", TermID = 1, CourseStartDate = new DateTime(2021, 1, 1), CourseEndDate = new DateTime(2021, 1, 31), CourseStatus = "Started", InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", Notes = "N/A" });
                db.Insert(new Course() { CourseID = 6, CourseTitle = "Course 6", TermID = 1, CourseStartDate = new DateTime(2021, 2, 1), CourseEndDate = new DateTime(2021, 1, 28), CourseStatus = "Pending", InstructorName = "Stephen Cherry", InstructorPhone = "111-1111", InstructorEmail = "scherr3@wgu.edu", Notes = "N/A" });

                db.Insert(new Assessment() { CourseID = 1, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentStartDate = new DateTime(2021, 1, 1), AssessmentEndDate = new DateTime(2021, 2, 1) });
                db.Insert(new Assessment() { CourseID = 1, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentStartDate = new DateTime(2021, 2, 1), AssessmentEndDate = new DateTime(2021, 3, 1) });
                db.Insert(new Assessment() { CourseID = 2, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentStartDate = new DateTime(2021, 1, 1), AssessmentEndDate = new DateTime(2021, 2, 1) });
                db.Insert(new Assessment() { CourseID = 2, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentStartDate = new DateTime(2021, 2, 1), AssessmentEndDate = new DateTime(2021, 3, 1) });
                db.Insert(new Assessment() { CourseID = 3, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentStartDate = new DateTime(2021, 1, 1), AssessmentEndDate = new DateTime(2021, 2, 1) });
                db.Insert(new Assessment() { CourseID = 3, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentStartDate = new DateTime(2021, 2, 1), AssessmentEndDate = new DateTime(2021, 3, 1) });
                db.Insert(new Assessment() { CourseID = 4, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentStartDate = new DateTime(2021, 1, 1), AssessmentEndDate = new DateTime(2021, 2, 1) });
                db.Insert(new Assessment() { CourseID = 4, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentStartDate = new DateTime(2021, 2, 1), AssessmentEndDate = new DateTime(2021, 3, 1) });
                db.Insert(new Assessment() { CourseID = 5, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentStartDate = new DateTime(2021, 1, 1), AssessmentEndDate = new DateTime(2021, 2, 1) });
                db.Insert(new Assessment() { CourseID = 5, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentStartDate = new DateTime(2021, 2, 1), AssessmentEndDate = new DateTime(2021, 3, 1) });
                db.Insert(new Assessment() { CourseID = 6, AssessmentTitle = "Assessment 1", AssessmentType = AssessmentType.Objective, AssessmentStartDate = new DateTime(2021, 1, 1), AssessmentEndDate = new DateTime(2021, 2, 1) });
                db.Insert(new Assessment() { CourseID = 6, AssessmentTitle = "Assessment 2", AssessmentType = AssessmentType.Performance, AssessmentStartDate = new DateTime(2021, 2, 1), AssessmentEndDate = new DateTime(2021, 3, 1) });
            }
        }

        public async Task<int> AddAssessmentAsync(Assessment assessment)
        {
            DB.Insert(assessment);
            int AssessmentID = DB.Table<Assessment>().Last().AssessmentID;
            return await Task.FromResult(AssessmentID);
        }

        public async Task<int> AddCourseAsync(Course course)
        {
            DB.Insert(course);
            int CourseID = DB.Table<Course>().Last().CourseID;
            return await Task.FromResult(CourseID);
        }

        public async Task<int> AddTermAsync(Term term)
        {
            DB.Insert(term);
            int TermID = DB.Table<Term>().Last().TermID;
            return await Task.FromResult(TermID);
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

        public async Task<bool> UpdateAssessmentAsync(Assessment assessment, int id)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath.dbPath);
            Assessment tableAssessment = await GetAssessmentAsync(id);
            bool assessmentFound = tableAssessment != null;
            if (assessmentFound)
            {
                tableAssessment.AssessmentTitle = assessment.AssessmentTitle;
                tableAssessment.AssessmentStartDate = assessment.AssessmentStartDate;
                tableAssessment.AssessmentEndDate = assessment.AssessmentEndDate;
                db.Update(tableAssessment);
            }
            return await Task.FromResult(assessmentFound);
        }

        public async Task<bool> UpdateCourseAsync(Course course, int id)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath.dbPath);
            Course CourseTable = await GetCourseAsync(id);
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

        public async Task<bool> UpdateTermAsync(Term term, int id)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath.dbPath);
            Term tableTerm = await GetTermAsync(id);
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

        public Task<bool> DeleteCourseAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAssessmentAsync(Assessment assessment)
        {
            throw new NotImplementedException();
        }
    }
}