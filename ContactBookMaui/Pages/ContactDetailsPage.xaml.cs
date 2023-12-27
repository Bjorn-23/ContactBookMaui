using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactDetailsPage : ContentPage
{
    public ContactDetailsPage(PContactDetailsViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}