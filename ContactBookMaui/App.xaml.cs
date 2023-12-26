namespace ContactBookMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        /// <summary>
        /// Sets Initial height and static width of Application.
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 700;
            const int newHeight = 1000;

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
