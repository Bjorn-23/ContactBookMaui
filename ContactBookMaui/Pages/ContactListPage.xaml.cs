using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}


}