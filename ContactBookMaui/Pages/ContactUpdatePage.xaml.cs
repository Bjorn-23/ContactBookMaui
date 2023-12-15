using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactUpdatePage : ContentPage
{
	public ContactUpdatePage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}