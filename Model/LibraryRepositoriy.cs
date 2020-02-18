using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients;

namespace Model
{
	public class LibraryRepositoriy : IDisposable
	{
		private readonly LibraryContext libraryContext;

		public LibraryRepositoriy()
		{
			libraryContext = new LibraryContext();
		}

		public void Dispose()
		{
			SaveChanges();
			libraryContext?.Dispose();
		}

		public IEnumerable<Book> FindBooksBy_Author(Author author)
		{
			return libraryContext.Books.Where(b => b.Author == author).ToArray();
		}

		public IEnumerable<Book> FindBooksBy_Genre(BookGenre bookGenre)
		{
			return libraryContext.Books.Where(b => b.Genre == bookGenre).ToArray();
		}

		public IEnumerable<Book> FindBooksBy_Year(int year)
		{
			return libraryContext.Books.Where(b => b.Year == year).ToArray();
		}

		public IEnumerable<Book> FindBooksBy_AuthorName(string name)
		{
			return libraryContext.Books.Where(a => a.Author.FirstName.Contains(name) || a.Author.LastName.Contains(name));
		}

		public IEnumerable<Book> FindBooksBy_Title(string title)
		{
			return libraryContext.Books.Where(b => b.Title.Contains(title)).ToArray();
		}

		public Book FindBookBy_Guid(Guid guid)
		{
			return libraryContext.Books.FirstOrDefault(b => b.BookId == guid);
		}

		public void Add(Book book)
		{
			libraryContext.Books.Add(book);
			Console.WriteLine("The book has been added successfully.\nPress any key to continue.");
			Console.Read();
		}

		//новый синтаксис
		public void Remove(Book book) => libraryContext.Books.Remove(book);

		public void RemoveById(Guid id)
		{
			var book = FindBookBy_Guid(id);
			libraryContext.Books.Remove(book);
			if (FindBookBy_Guid(id) != null)
				Console.WriteLine("The book has been deleted successfully.\nPress any key to continue.");
			else
				Console.WriteLine("Problem! The book has not been deleted.\nPress any key to continue.");
			Console.Read();
		}

		public void AddClient(string clientName)
		{
			libraryContext.Clients.Add(new Client(clientName));
			Console.WriteLine("You have been added to database successfully.\nPress any key to continue.");
			Console.Read();
		}

		public bool IsClientExistits(string name)
		{
			return libraryContext.Clients.Any(x => x.Name == name);
		}

		public Client GetClient(string name)
		{
			return libraryContext.Clients.Single(x => x.Name == name);
		}

		public bool DoesClientHasAnyBorrowed(Client client)
		{
			var cl = libraryContext.Clients.Single(c => c.ClientId == client.ClientId);
			bool isExist = cl.History.Any(h => h.ReturnDate == null);//.FirstOrDefault();
			return isExist;
		}

		public bool DidClientBorrowAnyEarlier(Client client)
		{
			var cl = libraryContext.Clients.Single(c => c.ClientId == client.ClientId);
			bool isExist = cl.History.Any();//.FirstOrDefault();
			return isExist;
		}

		public IEnumerable<BorrowedHistory> ViewBorrowedBooks(Client currentClient)
		{
			var client = libraryContext.Clients.Single(c => c.ClientId == currentClient.ClientId);
			return client.History;
		}

		/*
        public List<BorrowedHistory> ViewHistoryOfBorrowedBooks(Client client)
        {
            return libraryContext.BorrowedHistory.Where(x => x.Client.Name == client.Name).ToList();
        }
		*/

		public void SaveChanges()
		{
			try
			{
				libraryContext.SaveChanges();
				Console.WriteLine("Saved!");
			}
			catch (Exception e)
			{
				Console.WriteLine("Operation Failed.\nMessage: {0}\n{1}", e.Message, e.StackTrace);
			}
		}
	}
}