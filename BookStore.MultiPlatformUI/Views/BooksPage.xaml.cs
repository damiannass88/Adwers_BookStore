using BookStore.MultiPlatformUI.Models;
using BookStore.MultiPlatformUI.ViewModels;

namespace BookStore.MultiPlatformUI.Views;

public partial class BooksPage : ContentPage
{
	public BooksPage(BooksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedBook = e.CurrentSelection.FirstOrDefault() as Book;
		if (selectedBook != null)
		{
			await DisplayAlert("Book Selected", $"You selected: {selectedBook.Title}", "OK");
		}
	}
}