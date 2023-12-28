using ContactBookMaui.ViewModels;
using Microsoft.Maui.ApplicationModel;

namespace ContactBookMaui.Pages;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(PContactListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}