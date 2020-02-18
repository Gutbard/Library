using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Clients
{
	public class Client
	{
		public int ClientId { get; set; }
		public string Name { get; set; }

		//public List<Book> BorrowedBooks { get; set; }
		//virtual - lazy load linked to object - and class public - creates dynamic class with overloads
		//actual in small Dbs
		//in C# can be created dynamic classes
		public virtual List<BorrowedHistory> History { get; set; }

		public Client()
		{
			History = new List<BorrowedHistory>();
		}

		public Client(string name)
		{
			Name = name;
			History = new List<BorrowedHistory>();
		}

		//public void BorrowBook(Book book)
		//{
		//	//BorrowedBooks.Add(book);

		//}

		public void ReturnBook(Book book, List<BorrowedHistory> borrowedHistories)
		{
			History = borrowedHistories;
			//BorrowedBooks.Remove(book);
			if (History.Any(x => x.Book == book && x.Client == this && !x.ReturnDate.HasValue))
			{
				var history = History.Where(x => x.Book == book && x.Client == this && !x.ReturnDate.HasValue).FirstOrDefault();
				history.ReturnDate = DateTime.Now;
			}
			else
			{
				Console.WriteLine("There are no borrowed books by {0}", Name);
			}
		}
	}
}