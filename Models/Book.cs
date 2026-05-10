using OperationalBackendProgrammingRepetition.Interfaces;

namespace OperationalBackendProgrammingRepetition.Models
{
    public class Book : LibraryItem, ISearchable
    {
        private string _isbn;
        private string _author;
        private string _title;

        public string ISBN => _isbn;
        public string Author => _author;
        public string Title => _title;


        public Book(string title, string author, int year, string isbn)  : 
            base(title, year)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(author)) throw new ArgumentNullException(nameof(author), "Author cannot be empty.");
            if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentNullException(nameof(isbn), "ISBN cannot be empty.");
            
            _title = title;
            _isbn = isbn;
            _author = author;
        }

        public override string GetItemType() => "Book";

        public bool MatchesQuery(string query)
        {
            string q = query.Trim().ToLower();
            return Title.ToLower().Contains(q) || Author.ToLower().Contains(q);
        }
    }
}