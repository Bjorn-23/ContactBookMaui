using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactUpdatePage : ContentPage
{
	public ContactUpdatePage(UpdateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}