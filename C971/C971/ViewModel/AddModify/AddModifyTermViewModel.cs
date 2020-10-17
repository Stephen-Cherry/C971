using C971.Models;
using System;

namespace C971.ViewModel
{
    public class AddModifyTermViewModel : BaseViewModel
    {
        public Term Term { get; set; }

        public bool IsNewTerm { get; set; }

        public String TermTitle
        {
            get { return Term.TermTitle; }
            set
            {
                Term.TermTitle = value;
                OnPropertyChanged();
            }
        }

        public DateTime TermStartDate
        {
            get { return Term.TermStartDate; }
            set
            {
                Term.TermStartDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime TermEndDate
        {
            get { return Term.TermEndDate; }
            set
            {
                Term.TermEndDate = value;
                OnPropertyChanged();
            }
        }

        public AddModifyTermViewModel(Term term = null)
        {
            IsNewTerm = term == null;

            Title = IsNewTerm ? "Add Term" : "Edit Term";
            Term = term ?? new Term();
        }
    }
}