using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactAddPage : ContentPage
{
	public ContactAddPage(AddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}