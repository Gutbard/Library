using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;
using Clients;

namespace Model
{
	public class LibraryContext : DbContext
	{
		public LibraryContext() : base("LibraryDB_work")
		{
			//Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LibraryContext>());
			//Database.Log = loginfo => Console.WriteLine(loginfo);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();
			modelBuilder.Entity<Book>().HasIndex(b => b.Title).IsUnique();
			modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(200);

			modelBuilder.Entity<Book>().HasIndex(b => b.Genre);
			//modelBuilder.Entity<Book>().HasIndex(a => new { a.Title, a.Author }).IsUnique();

			modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired();
			modelBuilder.Entity<Author>().HasIndex(a => a.FirstName);

			modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired();
			modelBuilder.Entity<Author>().HasIndex(a => a.LastName);
			modelBuilder.Entity<Author>().HasIndex(a => new { a.FirstName, a.LastName }).IsUnique();

			modelBuilder.Entity<Author>().Property(a => a.FirstName).HasMaxLength(100);
			modelBuilder.Entity<Author>().Property(a => a.MiddleName).HasMaxLength(100);
			modelBuilder.Entity<Author>().Property(a => a.LastName).HasMaxLength(100);

			modelBuilder.Entity<Client>().Property(a => a.Name).IsRequired();
			modelBuilder.Entity<Client>().HasIndex(a => a.Name).IsUnique();
			modelBuilder.Entity<Client>().Property(a => a.Name).HasMaxLength(100);

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Client> Clients { get; set; }
		//public DbSet<BorrowedHistory> BorrowedHistory { get; set; }
	}
}