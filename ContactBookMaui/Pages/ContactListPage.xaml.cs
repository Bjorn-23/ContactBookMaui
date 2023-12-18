using ContactBookMaui.ViewModels;

namespace ContactBookMaui.Pages;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(ListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        //viewModel.UpdateContactList();
    }
}