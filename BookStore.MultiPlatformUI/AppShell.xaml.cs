using BookStore.MultiPlatformUI.Views;

namespace BookStore.MultiPlatformUI
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(BooksPage), typeof(BooksPage));
		}
	}
}
