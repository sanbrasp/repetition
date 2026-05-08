namespace OperationalBackendProgrammingRepetition.Models
{
    public class Book
    {
        private string _title;
        private string _author;
        private int _isbn;
        private int _year;
        private bool _isAvailable;
        
        public string Title => _title;
        public string Author => _author;
        public int ISBN => _isbn;
        public int Year => _year;
        
        
        public Book(string title, string author, int year, int isbn)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be empty.");

            _title = title;
            _author = author;
            _year = year;
            _isbn = isbn;
            _isAvailable = true;
        }
        
        public bool SetAvailable()
        {
            if (_isAvailable == false)
                _isAvailable = true;
            return _isAvailable;
        }
        public bool SetUnavailable()
        {
            if (_isAvailable)
                _isAvailable = false;
            return _isAvailable;
        }

        public override string ToString()
        {
            return ($"Title: {_title}\n" +
                              $"Author: {_author}\n" +
                              $"Year: {_year}\n" +
                              $"ISBN: {_isbn}");
        }
    }
}