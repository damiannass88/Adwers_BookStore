using BookStore.MultiPlatformUI.ViewModels;

namespace BookStore.MultiPlatformUI.Views;

public partial class BooksPage : ContentPage
{
	public BooksPage(BooksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}