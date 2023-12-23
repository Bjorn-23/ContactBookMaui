using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactAddPage : ContentPage
{
	public ContactAddPage(PContactAddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}