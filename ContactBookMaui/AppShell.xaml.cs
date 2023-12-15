using ContactBookMaui.Pages;

namespace ContactBookMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ContactMainPage", typeof(ContactMainPage));
            Routing.RegisterRoute("ContactListAllPage", typeof(ContactListPage));
            Routing.RegisterRoute("ContactAddPage", typeof(ContactAddPage));
            Routing.RegisterRoute("ContactUpdatePage", typeof(ContactUpdatePage));
            Routing.RegisterRoute("ContactDeletePage", typeof(ContactDeletePage));

        }
    }
}
