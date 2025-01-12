namespace Cooked_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var mainPage = new MainPage();
            var navigationPage = new NavigationPage(mainPage);  // Umhüllt die MainPage mit Navigation
            return new Window(navigationPage);  // Das Fenster wird mit der NavigationPage erstellt
        }
    }
}
