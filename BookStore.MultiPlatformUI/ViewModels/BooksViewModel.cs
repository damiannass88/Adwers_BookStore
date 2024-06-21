using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookStore.API.Models;
using BookStore.MultiPlatformUI.Services;

namespace BookStore.MultiPlatformUI.ViewModels
{
	public class BooksViewModel : INotifyPropertyChanged
	{
		private readonly BookService _bookService;
		private List<Book> _books;

		public List<Book> Books
		{
			get => _books;
			set
			{
				_books = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public BooksViewModel(BookService bookService)
		{
			_bookService = bookService;
			LoadBooks();
		}

		private async void LoadBooks()
		{
			Books = await _bookService.GetBooksAsync();
		}
	}

}
