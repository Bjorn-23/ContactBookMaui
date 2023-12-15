using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactDeletePage : ContentPage
{
	public ContactDeletePage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}