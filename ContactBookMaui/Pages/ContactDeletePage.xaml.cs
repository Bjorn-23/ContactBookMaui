using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactDeletePage : ContentPage
{
	public ContactDeletePage(DeleteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}