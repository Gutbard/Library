using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Library
{


    internal class Program
    {
        /*
        1. Application for Librarians
        Features:
        - Find books by
            Author
            Genre
            Year
        - Add/remove books
        */

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

        private static void AddBook()
        {
            using (var repositoriy = new LibraryRepositoriy())
            {
                Console.WriteLine("Enter the book title: ");
                var title = Console.ReadLine();

                Console.WriteLine("Enter the book summary: ");
                var summary = Console.ReadLine();

                string yearStr; int year;
                do
                {
                    Console.WriteLine("Enter the book year: ");
                    yearStr = Console.ReadLine();
                } while (!int.TryParse(yearStr, out year));

                string genreStr; BookGenre genre;
                do
                {
                    Console.WriteLine("Enter genre: ");
                    Console.WriteLine("==List of available genres:==");
                    foreach (var g in Enum.GetNames(typeof(BookGenre)))
                    {
                        Console.WriteLine($"- { g}");
                    }
                    Console.WriteLine("==========================");
                    genreStr = Console.ReadLine();
                } while (!Enum.TryParse(genreStr, out genre));

                Console.WriteLine("Enter author name (First, Middle, Last names): ");
                var authorNames = Console.ReadLine();

                string FN, MN, LN;
                var names = authorNames.Split(' ');
                if (names.Length > 2)
                {
                    FN = names[0];
                    MN = names[1];
                    LN = names[2];
                }
                else
                {
                    FN = names[0];
                    MN = string.Empty;
                    LN = names[1];
                }

                string bdStr; DateTime DOB;
                do
                {
                    Console.WriteLine("Ener the Author birthday: ");
                    bdStr = Console.ReadLine();
                } while (!DateTime.TryParse(bdStr, out DOB));

                repositoriy.Add(new Book
                {
                    BookId = Guid.NewGuid(),
                    Title = title,
                    Summary = summary,
                    Year = year,
                    Genre = genre,
                    Author = new Author
                    {
                        FirstName = FN,
                        MiddleName = MN,
                        LastName = LN,
                        DateOfBirth = DOB
                    },
                    Client = null
                });
            }
        }

        private static void RemoveBook()
        {
            using (var repositoriy = new LibraryRepositoriy())
            {
                string guidStr; Guid guid;
                do
                {
                    Console.WriteLine("Enter book id to remove it from database: ");
                    guidStr = Console.ReadLine();
                } while (!Guid.TryParse(guidStr, out guid));

                repositoriy.RemoveById(guid);
            }
        }

        private static void Main(string[] args)
        {
            var MenuItems = new MenuItem[]
            {
                new MenuItem(1, "Find Book By Author", FindByAuthor),
                new MenuItem(2, "Find Book By Genre", FindByGenre),
                new MenuItem(3, "Find book by year", FindByYear),
                new MenuItem(4, "Add book", AddBook),
                new MenuItem(5, "Remove book", RemoveBook)
            };

            var menu = new Menu(MenuItems);
            menu.Process();

            //using (var repositoriy = new LibraryRepositoriy())
            //{
            //    //var books = repositoriy.FindByYear(2010).FirstOrDefault();
            //    //var author = books.Author;
            //    addRichter(repositoriy);

            //    //repositoriy.AddClient(new Client("asdb"));
            //}
        }

        public static void addRichter(LibraryRepositoriy repositoriy)
        {
            repositoriy.Add(new Book
            {
                BookId = Guid.NewGuid(),
                Title = "C# via CLR",
                Year = 2010,
                Summary = "Dig deep and master the intricacies of the common language runtime, C#, and .NET development. Led by programming expert Jeffrey Richter, a longtime consultant to the Microsoft .NET team - you’ll gain pragmatic insights for building robust, reliable, and responsive apps and components.",
                Genre = BookGenre.Drama,
                Client = null,
                Author = new Author
                {
                    FirstName = "Jeffrey",
                    LastName = "Richter",
                    DateOfBirth = new DateTime(1957, 5, 29)
                }
            });
        }
    }
}