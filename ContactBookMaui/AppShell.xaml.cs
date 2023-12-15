using ContactBookMaui.Pages;

namespace ContactBookMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ContactListPage", typeof(ContactListPage));
            Routing.RegisterRoute("ContactAddPage", typeof(ContactAddPage));
        }
    }
}
