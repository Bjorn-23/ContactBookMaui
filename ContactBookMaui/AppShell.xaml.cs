using ContactBookMaui.Pages;

namespace ContactBookMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        //Routing.RegisterRoute(nameof(ContactMainPage), typeof(ContactMainPage));
        Routing.RegisterRoute(nameof(ContactListPage), typeof(ContactListPage));
        Routing.RegisterRoute(nameof(ContactAddPage), typeof(ContactAddPage));
        Routing.RegisterRoute(nameof(ContactUpdatePage), typeof(ContactUpdatePage));
        Routing.RegisterRoute(nameof(ContactDeletePage), typeof(ContactDeletePage));

    }
}
