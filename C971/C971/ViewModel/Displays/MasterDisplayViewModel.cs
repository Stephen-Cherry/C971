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
    internal class MasterDisplayViewModel : BaseViewModel
    {
        public ObservableCollection<Term> Terms { get; set; }
        public Command LoadTermsCommand { get; set; }

        public MasterDisplayViewModel()
        {
            Title = "Terms";
            Terms = new ObservableCollection<Term>();
            LoadTermsCommand = new Command(async () => await ExecuteLoadTermsCommand());

            MessagingCenter.Subscribe<AddModifyTermPage, Term>(this, "SaveTerm",
    async (sender, term) =>
    {
        Terms.Add(term);
        await DataStore.AddTermAsync(term);
    });

            MessagingCenter.Subscribe<AddModifyTermPage, Term>(this, "UpdateTerm",
                async (sender, term) =>
                {
                    await DataStore.UpdateTermAsync(term, term.TermID);
                    await ExecuteLoadTermsCommand();
                });

            MessagingCenter.Subscribe<TermDisplayPage, Term>(this, "DeleteTerm",
    async (sender, term) =>
    {
        await DataStore.DeleteTermAsync(term);
        await ExecuteLoadTermsCommand();
    });
        }

        private async Task ExecuteLoadTermsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Terms.Clear();
                IList<Term> terms = await DataStore.GetTermsAsync();
                foreach (Term term in terms)
                {
                    Terms.Add(term);
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