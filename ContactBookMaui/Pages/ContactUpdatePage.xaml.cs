using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactUpdatePage : ContentPage
{
	public ContactUpdatePage(PContactUpdateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}