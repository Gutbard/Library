using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients;

namespace Model
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Year { get; set; }
        public BookGenre Genre { get; set; }

        //for lazy loading
        public virtual Author Author { get; set; }

        public Client Client { get; set; }
    }
}