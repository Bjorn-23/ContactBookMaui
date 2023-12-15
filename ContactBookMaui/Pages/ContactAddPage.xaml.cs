using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactAddPage : ContentPage
{
	public ContactAddPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}