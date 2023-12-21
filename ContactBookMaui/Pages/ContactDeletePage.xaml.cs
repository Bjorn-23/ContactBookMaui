using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactDeletePage : ContentPage
{
	public ContactDeletePage(PContactDeleteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}