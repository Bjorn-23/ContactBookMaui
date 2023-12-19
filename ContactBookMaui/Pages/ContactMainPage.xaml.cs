using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactMainPage : ContentPage
{
	public ContactMainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}