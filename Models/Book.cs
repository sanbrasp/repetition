using OperationalBackendProgrammingRepetition.Interfaces;

namespace OperationalBackendProgrammingRepetition.Models
{
    public class Book : LibraryItem, ISearchable
    {
        private int _isbn;
        private string _author;
        private string _title;

        public int ISBN => _isbn;
        public string Author => _author;
        public string Title => _title;


        public Book(string title, string author, int year, int isbn)  : 
            base(title, year)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be empty.");
            if (isbn < 0) throw new ArgumentOutOfRangeException(nameof(isbn), "ISBN cannot be less than 0.");
            
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