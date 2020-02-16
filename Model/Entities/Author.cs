using System;

namespace Model
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }
    }
}