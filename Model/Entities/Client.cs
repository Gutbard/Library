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
        }

        public Client(string name)
        {
            Name = name;
        }

        public void BorrowBook(Book book)
        {
            //BorrowedBooks = new List<Book>();
            History = new List<BorrowedHistory>();

            //BorrowedBooks.Add(book);

            BorrowedHistory bh = new BorrowedHistory
            {
                Book = book,
                Client = this,
                BorrowDate = DateTime.Now
            };

            History.Add(bh);

            Console.WriteLine($"{book.Title} has been Borrowed by {this.Name}.\nPress any key to contnue.");
            Console.Read();
        }

        public void ReturnBook(Book book, List<BorrowedHistory> borrowedHistories)
        {
            History = borrowedHistories;
            //BorrowedBooks.Remove(book);
            if (History.Any(x => x.Book == book && x.Client == this && !x.ReturnDate.HasValue))
            {
                History.Where(x => x.Book == book && x.Client == this && !x.ReturnDate.HasValue).FirstOrDefault().ReturnDate = DateTime.Now;
            }

            Console.WriteLine($"{book.Title} has been returned to Library.\nPress any key to contnue.");
            Console.Read();
        }
    }
}