using C971.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace C971.Services
{
    public interface IDataStore
    {
        Task<int> AddTermAsync(Term term);
        Task<bool> UpdateTermAsync(Term term);
        Task<Term> GetTermAsync(int id);
        Task<IList<Term>> GetTermsAsync();
        Task<bool> DeleteTermAsync(Term term);

        Task<int> AddCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Course course);
        Task<Course> GetCourseAsync(int id);
        Task<IList<Course>> GetCoursesAsync();
        Task<bool> DeleteCourseAsync(Course course);
        Task<IList<string>> GetCourseStatusesAsync();

        Task<int> AddAssessmentAsync(Assessment assessment);
        Task<bool> UpdateAssessmentAsync(Assessment assessment);
        Task<Assessment> GetAssessmentAsync(int id);
        Task<IList<Assessment>> GetAssessmentsAsync();
        Task<bool> DeleteAssessmentAsync(Assessment assessment);
        Task<IList<AssessmentType>> GetAssessmentTypesAsync();
    }
}
