using TODOApplication.Views;
using Xamarin.Forms;

namespace TODOApplication {
  public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TodoListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
