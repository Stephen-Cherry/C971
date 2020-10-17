using C971.Models;
using C971.Services;
using C971.Views;
using SQLite;
using System;
using Xamarin.Forms;

namespace C971
{
    //TODO: Note Sharing
    //TODO: Notifications for the Start AND End dates for each Course
    //TODO: Notifications for the Start AND End dates for each Assessment
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TermDisplayPage());
        }

        protected override void OnStart()
        {
            SQLiteDataStore.FirstLaunch = true;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}