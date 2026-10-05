using ASP_BookLibrary.Models;

namespace ASP_BookLibrary.Controllers
{
    public class BookController
    {
        private List<Book> _books;
        private int _lastID = 0;

        public BookController()
        {
            _books = new();
            LoadingData();           
        }

        public bool TryGetBookByID(int id, out Book find)
        {
            find = _books.FirstOrDefault(b => b.Id == id);
            return find != null;
        }

        public string GetStringBooks()
        {
            if (_books.Count == 0)
            {
                return "No books available.";
            }
            string result = "Available Books:\n";
            foreach (var book in _books)
            {
                result += $"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}\n";
            }
            return result;
        }

        public int AddBook(Book book)
        {
            book.Id = ++_lastID;
            _books.Add(book);

            return book.Id; 
        }

        public bool RemoveBook(Book book)
        {
            return _books.Remove(book);
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            return _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private void LoadingData()
        {
            // Имитация загрузки данных из базы данных или другого источника
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3" }
            };

            _lastID = _books.Max(b => b.Id);
        }
    }
}
