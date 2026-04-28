namespace NetLearnSamples
{
    public record class Book(string Title, string Author, int YearPublished, string Genre);

    public class BookStore : List<Book>
    {
        public void AddBook(string title, string author, int yearPublished, string genre)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            if (this.Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A book with the title '{title}' already exists in the store.");
            }

            var book = new Book(title, author, yearPublished, genre);
            Add(book);
        }

        public void RemoveBook(string title)
        {
            var bookToRemove = this.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (bookToRemove == null)
            {
                throw new InvalidOperationException($"No book with the title '{title}' found in the store.");
            }
            Remove(bookToRemove);
        }

        public void DisplayBooks()
        {
            foreach (var book in this)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.YearPublished}, Genre: {book.Genre}");
            }
        }

        public void SortByTitle()
        {
            Sort((b1, b2) => string.Compare(b1.Title, b2.Title));
        }
    }
}
