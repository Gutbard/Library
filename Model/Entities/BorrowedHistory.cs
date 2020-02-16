using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Clients
{
    public class BorrowedHistory
    {
        public int BorrowedHistoryId { get; set; }
        public virtual Book Book { get; set; }
        public Client Client { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}