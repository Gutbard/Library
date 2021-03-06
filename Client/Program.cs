﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Clients;

namespace Library
{
	internal class Program
	{
		/*
		2. Application for Clients
		Features:
			- Borrow/return book
			- View borrowed books
			- View history of borrowed books
			*/

		/*
	private static void FindByAuthor()
	{
		using (var repositoriy = new LibraryRepositoriy())
		{
			Console.WriteLine("Enter the AuthorName: ");
			var name = Console.ReadLine();
			var authors = repositoriy.FindBooksBy_AuthorName(name).ToList();
			if (authors.Count > 0)
			{
				int i = 1;
				foreach (var item in authors)
				{
					Console.WriteLine($"Bookid={item.BookId}\n{i++}. {item.Title} ({item.Year})\t{item.Genre}\t{item.Author.ToString()}\n{item.Summary}");
				}
			}
			else Console.WriteLine("Sorry, nothing found.");
			Console.WriteLine("\nPress any key to continue.");
			Console.Read();
		}
	}

	private static void FindByGenre()
	{
		using (var repositoriy = new LibraryRepositoriy())
		{
			Console.WriteLine("Enter Genre: ");
			int i = 0;
			foreach (var g in Enum.GetNames(typeof(BookGenre)))
			{
				Console.WriteLine($"{ i++}.{ g}");
			}
			var name = Console.ReadLine();
			if (int.TryParse(name, out int genreid))
			{
				var books = repositoriy.FindBooksBy_Genre((BookGenre)genreid).ToList();
				if (books.Count > 0)
				{
					int bookNo = 1;
					foreach (var item in books)
					{
						Console.WriteLine($"Bookid={item.BookId}\n{bookNo++}. {item.Title} ({item.Year})\t{item.Genre}\t{item.Author.ToString()}\n{item.Summary}");
					}
				}
			}
			else { Console.WriteLine("Sorry, nothing found."); }
			Console.WriteLine("\nPress any key to continue.");
			Console.Read();
		}
	}

	private static void FindByYear()
	{
		using (var repositoriy = new LibraryRepositoriy())
		{
			Console.WriteLine("Enter the year: ");
			var name = Console.ReadLine();
			if (int.TryParse(name, out int year))
			{
				var books = repositoriy.FindBooksBy_Year(year).ToList();

				if (books.Count > 0)
				{
					int i = 1;
					foreach (var item in books)
					{
						Console.WriteLine($"Bookid={item.BookId}\n{i++}. {item.Title} ({item.Year})\t{item.Genre}\t{item.Author.ToString()}\n{item.Summary}");
					}
				}
				else Console.WriteLine("Sorry, nothing found.");
				Console.WriteLine("\nPress any key to continue.");
				Console.Read();
			}
		}
	}
	*/

		public static Client _currentClient;

		private static void BorrowBook()
		{
			using (var repositoriy = new LibraryRepositoriy())
			{
				Console.WriteLine("Enter book title: ");
				var title = Console.ReadLine();

				Console.WriteLine("Enter book year: ");

				var year = int.Parse(Console.ReadLine());

				var book = repositoriy.FindBooksBy_Year(year).Where(b => b.Title.Contains(title)).FirstOrDefault();

				if (book == null)
					Console.WriteLine("book not found");
				else if (book.Client != null)
				{//this part has a bug
					Console.WriteLine("This book is borrowed");
				}
				else
				{
					Console.WriteLine("Borrowing...");
					//repositoriy.BorrowBook(book, _currentClient);
					var currentClient = repositoriy.GetClient(_currentClient.Name);
					book.Client = currentClient;
					currentClient.History = new List<BorrowedHistory>();
					currentClient.History.Add(new BorrowedHistory
					{
						Book = book,
						Client = currentClient,
						BorrowDate = DateTime.Now,
					});

					Console.WriteLine($"{book.Title} has been Borrowed by {currentClient.Name}.\nPress any key to contnue.");
					Console.Read();
				}
				Console.WriteLine("Press any key to continue!");
				Console.Read();
			}
		}

		private static void ReturnBook()
		{
			using (var repositoriy = new LibraryRepositoriy())
			{
				if (repositoriy.DoesClientHasAnyBorrowed(_currentClient))
				{
					Console.WriteLine("enter borrowed id: ");
					var id = int.Parse(Console.ReadLine());

					var history = repositoriy.ViewBorrowedBooks(_currentClient).Single(h => h.BorrowedHistoryId == id);
					history.ReturnDate = DateTime.Now;
					history.Book.Client = null;
					Console.WriteLine($"{history.Book.Title} has been returned to Library.\nPress any key to contnue.");
				}
				else
				{
					Console.WriteLine("There are no borrowed books by {0} ", _currentClient.Name);
				}
				Console.Read();
			}
		}

		private static void ViewBorrowedBooks()
		{
			using (var repositoriy = new LibraryRepositoriy())
			{
				if (repositoriy.DoesClientHasAnyBorrowed(_currentClient))
				{
					var borrowedBooks = repositoriy.ViewBorrowedBooks(_currentClient)?.Where(h => h.ReturnDate == null);
					foreach (var b in borrowedBooks)
					{
						Console.WriteLine($"{b.BorrowedHistoryId}\t'{b.Book.Title}'\tBorrowDate: {b.BorrowDate}");
					}
				}
				else { Console.WriteLine("There are no borrowed books."); }
				Console.Read();
			}
		}

		private static void ViewHistoryBorrowedBooks()
		{
			using (var repositoriy = new LibraryRepositoriy())
			{
				if (repositoriy.DidClientBorrowAnyEarlier(_currentClient))
				{
					var borrowedBooks = repositoriy.ViewBorrowedBooks(_currentClient);
					foreach (var b in borrowedBooks)
					{
						Console.WriteLine($"{b.BorrowedHistoryId}\t'{b.Book.Title}'\tBorrowDate: {b.BorrowDate}, ReturnDate: {b.ReturnDate}");
					}
				}
				else Console.WriteLine("No borrowed books.");
				Console.Read();
			}
		}

		private static void Login()
		{
			Console.WriteLine("Please enter your name: ");
			var name = Console.ReadLine();

			using (var repositoriy = new LibraryRepositoriy())
			{
				if (!repositoriy.IsClientExistits(name))
				{
					if (!string.IsNullOrEmpty(name))
						repositoriy.AddClient(name);
				}
			}
			using (var repositoriy = new LibraryRepositoriy())
			{
				_currentClient = repositoriy.GetClient(name);
				Console.WriteLine("Hi, {0}!", _currentClient.Name);
			}
		}

		private static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Library client app.");

			Login();

			var MenuItems = new MenuItem[]
			{
				new MenuItem(1, "BorrowBook", BorrowBook),
				new MenuItem(2, "ReturnBook", ReturnBook),
				new MenuItem(3, "ViewBorrowedBooks", ViewBorrowedBooks),
				new MenuItem(4, "ViewHistoryBorrowedBooks", ViewHistoryBorrowedBooks),
			};

			var menu = new Menu(MenuItems);
			menu.Process();

			//using (var repositoriy = new LibraryRepositoriy())
			//{
			/*
                //Borrow
                if (item == 1)
                {
                  //  Menu();
                    string guidStr; Guid guid;
                    do
                    {
                        Console.WriteLine("Enter the Guid of the Book which you want to borrow: ");
                        guidStr = Console.ReadLine();
                    } while (!Guid.TryParse(guidStr, out guid));

                    Book book = repositoriy.FindBookBy_Guid(guid);

                    repositoriy.BorrowBook(book, client);
                }*/
			//Return
			/*
			else
			{
			   // Menu();
				string guidStr; Guid guid;
				do
				{
					Console.WriteLine("Enter the Guid of the Book which you want to return: ");
					guidStr = Console.ReadLine();
				} while (!Guid.TryParse(guidStr, out guid));

				Book book = repositoriy.FindBookBy_Guid(guid);

				repositoriy.ReturnBook(book, client);
			}
			*/
			//}
			/*
            void Menu()
            {
                string findStr; int find;
                do
                {
                    Console.WriteLine("Select an action:\n1.FindByAuthor\n2.FindByGenre\n3.FindByYear");
                    findStr = Console.ReadLine();
                } while (!int.TryParse(findStr, out find));
                switch (find)
                {
                    case (1): FindByAuthor(); break;
                    case (2): FindByGenre(); break;
                    case (3): FindByYear(); break;
                    default: Console.WriteLine(""); break;
                }
            }
			*/
		}
	}
}