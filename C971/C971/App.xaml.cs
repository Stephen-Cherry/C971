using C971.Services;
using C971.ViewModel;
using C971.Views;
using Xamarin.Forms;

namespace C971
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MasterDisplayPage());
        }

        protected override void OnStart()
        {
            SQLiteDataStore.FirstLaunch = true;
            MasterDisplayViewModel.FirstLaunch = true;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}