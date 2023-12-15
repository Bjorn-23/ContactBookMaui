namespace ContactBookMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 680;
            const int newHeight = 800;

            //Sets initial width and height of window - but is resizable
            //window.Width = newWidth;
            window.Height = newHeight;

            //Sets static size of window - not resizable
            window.MinimumWidth = window.MaximumWidth = window.Width = newWidth;
            //window.MinimumHeight = window.MaximumHeight = window.Height = newHeight;

            return window;
        }
    }

}
