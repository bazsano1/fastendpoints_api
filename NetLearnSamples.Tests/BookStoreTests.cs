using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NetLearnSamples.Tests
{
    public class BookStoreTests
    {
        // ========== AddBook Tests ==========

        [Fact]
        public void AddBook_WithValidBook_AddsBookToStore()
        {
            // Arrange
            var bookStore = new BookStore();
            var title = "The Great Gatsby";
            var author = "F. Scott Fitzgerald";
            var yearPublished = 1925;
            var genre = "Novel";

            // Act
            bookStore.AddBook(title, author, yearPublished, genre);

            // Assert
            Assert.Single(bookStore);
            var addedBook = bookStore[0];
            Assert.Equal(title, addedBook.Title);
            Assert.Equal(author, addedBook.Author);
            Assert.Equal(yearPublished, addedBook.YearPublished);
            Assert.Equal(genre, addedBook.Genre);
        }

        [Fact]
        public void AddBook_WithMultipleBooks_AddsAllBooksToStore()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("The Great Gatsby", "F. Scott Fitzgerald", 1925, "Novel");
            bookStore.AddBook("To Kill a Mockingbird", "Harper Lee", 1960, "Novel");
            bookStore.AddBook("1984", "George Orwell", 1949, "Dystopian");

            // Assert
            Assert.Equal(3, bookStore.Count);
        }

        [Fact]
        public void AddBook_WithNullTitle_ThrowsArgumentException()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                bookStore.AddBook(null, "Author", 2020, "Fiction"));
            Assert.Equal("Title cannot be null or empty. (Parameter 'title')", exception.Message);
        }

        [Fact]
        public void AddBook_WithEmptyTitle_ThrowsArgumentException()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                bookStore.AddBook("", "Author", 2020, "Fiction"));
            Assert.Equal("Title cannot be null or empty. (Parameter 'title')", exception.Message);
        }

        [Fact]
        public void AddBook_WithWhitespaceTitle_ThrowsArgumentException()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                bookStore.AddBook("   ", "Author", 2020, "Fiction"));
            Assert.Equal("Title cannot be null or empty. (Parameter 'title')", exception.Message);
        }

        [Fact]
        public void AddBook_WithDuplicateTitle_ThrowsInvalidOperationException()
        {
            // Arrange
            var bookStore = new BookStore();
            var title = "The Great Gatsby";
            bookStore.AddBook(title, "F. Scott Fitzgerald", 1925, "Novel");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                bookStore.AddBook(title, "Different Author", 2020, "Fiction"));
            Assert.Equal($"A book with the title '{title}' already exists in the store.", exception.Message);
        }

        [Fact]
        public void AddBook_WithDuplicateTitleDifferentCase_ThrowsInvalidOperationException()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("The Great Gatsby", "F. Scott Fitzgerald", 1925, "Novel");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                bookStore.AddBook("the great gatsby", "Different Author", 2020, "Fiction"));
            Assert.Contains("already exists in the store", exception.Message);
        }

        [Fact]
        public void AddBook_WithValidAuthor_AddsBookWithAuthor()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("Test Book", "John Doe", 2020, "Fiction");

            // Assert
            Assert.Equal("John Doe", bookStore[0].Author);
        }

        [Fact]
        public void AddBook_WithNullAuthor_AddsBookWithNullAuthor()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("Test Book", null, 2020, "Fiction");

            // Assert
            Assert.Null(bookStore[0].Author);
        }

        [Fact]
        public void AddBook_WithValidYear_AddsBookWithYear()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("Test Book", "Author", 1999, "Fiction");

            // Assert
            Assert.Equal(1999, bookStore[0].YearPublished);
        }

        [Fact]
        public void AddBook_WithValidGenre_AddsBookWithGenre()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("Test Book", "Author", 2020, "Science Fiction");

            // Assert
            Assert.Equal("Science Fiction", bookStore[0].Genre);
        }

        [Fact]
        public void AddBook_WithNullGenre_AddsBookWithNullGenre()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("Test Book", "Author", 2020, null);

            // Assert
            Assert.Null(bookStore[0].Genre);
        }

        // ========== SortByTitle Tests ==========

        [Fact]
        public void SortByTitle_WithMultipleBooks_SortsBooksAlphabetically()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Zebra Adventures", "Author Z", 2020, "Fiction");
            bookStore.AddBook("Apple Tales", "Author A", 2020, "Fiction");
            bookStore.AddBook("Banana Stories", "Author B", 2020, "Fiction");

            // Act
            bookStore.SortByTitle();

            // Assert
            Assert.Equal("Apple Tales", bookStore[0].Title);
            Assert.Equal("Banana Stories", bookStore[1].Title);
            Assert.Equal("Zebra Adventures", bookStore[2].Title);
        }

        [Fact]
        public void SortByTitle_WithSingleBook_RemainsSorted()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Only Book", "Author", 2020, "Fiction");

            // Act
            bookStore.SortByTitle();

            // Assert
            Assert.Single(bookStore);
            Assert.Equal("Only Book", bookStore[0].Title);
        }

        [Fact]
        public void SortByTitle_WithEmptyStore_DoesNotThrow()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act & Assert (should not throw)
            bookStore.SortByTitle();
            Assert.Empty(bookStore);
        }

        [Fact]
        public void SortByTitle_WithTwoBooks_SortedCorrectly()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Zebra", "Author", 2020, "Fiction");
            bookStore.AddBook("Apple", "Author", 2020, "Fiction");

            // Act
            bookStore.SortByTitle();

            // Assert
            Assert.Equal("Apple", bookStore[0].Title);
            Assert.Equal("Zebra", bookStore[1].Title);
        }

        [Fact]
        public void SortByTitle_WithCaseSensitivity_SortsByStringCompare()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("apple", "Author", 2020, "Fiction");
            bookStore.AddBook("Banana", "Author", 2020, "Fiction");
            bookStore.AddBook("Cherry", "Author", 2020, "Fiction");

            // Act
            bookStore.SortByTitle();

            // Assert
            // String.Compare is case-insensitive by default in this context
            Assert.Equal(3, bookStore.Count);
            // Verify sorting occurred
            Assert.True(bookStore[0].Title.CompareTo(bookStore[1].Title) <= 0);
            Assert.True(bookStore[1].Title.CompareTo(bookStore[2].Title) <= 0);
        }

        // ========== DisplayBooks Tests ==========

        [Fact]
        public void DisplayBooks_WithBooks_DoesNotThrow()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("The Great Gatsby", "F. Scott Fitzgerald", 1925, "Novel");
            bookStore.AddBook("To Kill a Mockingbird", "Harper Lee", 1960, "Novel");

            // Act & Assert (should not throw)
            bookStore.DisplayBooks();
        }

        [Fact]
        public void DisplayBooks_WithEmptyStore_DoesNotThrow()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act & Assert (should not throw)
            bookStore.DisplayBooks();
        }

        // ========== List<Book> Inheritance Tests ==========

        [Fact]
        public void BookStore_InheritsFromListBook_CanIterateBooks()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book 1", "Author 1", 2020, "Fiction");
            bookStore.AddBook("Book 2", "Author 2", 2021, "Mystery");

            // Act
            var books = bookStore.ToList();

            // Assert
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public void BookStore_CanUseLinqQueries_FiltersBooks()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Fiction Book", "Author", 2020, "Fiction");
            bookStore.AddBook("Mystery Book", "Author", 2021, "Mystery");
            bookStore.AddBook("Another Fiction", "Author", 2022, "Fiction");

            // Act
            var fictionBooks = bookStore.Where(b => b.Genre == "Fiction").ToList();

            // Assert
            Assert.Equal(2, fictionBooks.Count);
        }

        [Fact]
        public void BookStore_CanCountBooks_ReturnsCorrectCount()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book 1", "Author", 2020, "Fiction");
            bookStore.AddBook("Book 2", "Author", 2021, "Fiction");
            bookStore.AddBook("Book 3", "Author", 2022, "Fiction");

            // Act
            var count = bookStore.Count;

            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public void BookStore_CanAccessBookByIndex_ReturnsCorrectBook()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("First Book", "Author 1", 2020, "Fiction");
            bookStore.AddBook("Second Book", "Author 2", 2021, "Mystery");

            // Act
            var firstBook = bookStore[0];
            var secondBook = bookStore[1];

            // Assert
            Assert.Equal("First Book", firstBook.Title);
            Assert.Equal("Second Book", secondBook.Title);
        }

        // ========== Integration Tests ==========

        [Fact]
        public void Bookstore_CompleteWorkflow_AddMultipleSortAndRetrieve()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act
            bookStore.AddBook("Zebra Book", "Author Z", 2020, "Fiction");
            bookStore.AddBook("Apple Book", "Author A", 2020, "Fiction");
            bookStore.AddBook("Banana Book", "Author B", 2020, "Fiction");
            bookStore.SortByTitle();

            // Assert
            Assert.Equal(3, bookStore.Count);
            Assert.Equal("Apple Book", bookStore[0].Title);
            Assert.Equal("Banana Book", bookStore[1].Title);
            Assert.Equal("Zebra Book", bookStore[2].Title);
        }

        [Fact]
        public void BookStore_RemoveBook_ReducesCount()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book to Remove", "Author", 2020, "Fiction");
            bookStore.AddBook("Book to Keep", "Author", 2021, "Fiction");

            // Act
            var bookToRemove = bookStore[0];
            bookStore.Remove(bookToRemove);

            // Assert
            Assert.Single(bookStore);
            Assert.Equal("Book to Keep", bookStore[0].Title);
        }

        [Fact]
        public void BookStore_ClearAll_ResultsInEmptyStore()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book 1", "Author", 2020, "Fiction");
            bookStore.AddBook("Book 2", "Author", 2021, "Fiction");

            // Act
            bookStore.Clear();

            // Assert
            Assert.Empty(bookStore);
        }

        // ========== RemoveBook Tests ==========

        [Fact]
        public void RemoveBook_WithExistingBook_RemovesBookFromStore()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("The Great Gatsby", "F. Scott Fitzgerald", 1925, "Novel");
            bookStore.AddBook("To Kill a Mockingbird", "Harper Lee", 1960, "Novel");

            // Act
            bookStore.RemoveBook("The Great Gatsby");

            // Assert
            Assert.Single(bookStore);
            Assert.Equal("To Kill a Mockingbird", bookStore[0].Title);
        }

        [Fact]
        public void RemoveBook_WithNonExistingBook_ThrowsInvalidOperationException()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Existing Book", "Author", 2020, "Fiction");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                bookStore.RemoveBook("Non-Existing Book"));
            Assert.Equal("No book with the title 'Non-Existing Book' found in the store.", exception.Message);
        }

        [Fact]
        public void RemoveBook_WithCaseDifference_RemovesBookCaseInsensitive()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("The Great Gatsby", "F. Scott Fitzgerald", 1925, "Novel");

            // Act
            bookStore.RemoveBook("the great gatsby");

            // Assert
            Assert.Empty(bookStore);
        }

        [Fact]
        public void RemoveBook_WithPartialCaseMatch_RemovesBookCaseInsensitive()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("The Great Gatsby", "F. Scott Fitzgerald", 1925, "Novel");

            // Act
            bookStore.RemoveBook("THE GREAT GATSBY");

            // Assert
            Assert.Empty(bookStore);
        }

        [Fact]
        public void RemoveBook_WithMultipleBooks_RemovesCorrectBook()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book A", "Author A", 2020, "Fiction");
            bookStore.AddBook("Book B", "Author B", 2021, "Mystery");
            bookStore.AddBook("Book C", "Author C", 2022, "Fantasy");

            // Act
            bookStore.RemoveBook("Book B");

            // Assert
            Assert.Equal(2, bookStore.Count);
            Assert.Contains(bookStore, b => b.Title == "Book A");
            Assert.Contains(bookStore, b => b.Title == "Book C");
            Assert.DoesNotContain(bookStore, b => b.Title == "Book B");
        }

        [Fact]
        public void RemoveBook_RemovingFirstBook_CorrectlyUpdatesStore()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("First Book", "Author", 2020, "Fiction");
            bookStore.AddBook("Second Book", "Author", 2021, "Fiction");
            bookStore.AddBook("Third Book", "Author", 2022, "Fiction");

            // Act
            bookStore.RemoveBook("First Book");

            // Assert
            Assert.Equal(2, bookStore.Count);
            Assert.Equal("Second Book", bookStore[0].Title);
            Assert.Equal("Third Book", bookStore[1].Title);
        }

        [Fact]
        public void RemoveBook_RemovingLastBook_CorrectlyUpdatesStore()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("First Book", "Author", 2020, "Fiction");
            bookStore.AddBook("Second Book", "Author", 2021, "Fiction");
            bookStore.AddBook("Third Book", "Author", 2022, "Fiction");

            // Act
            bookStore.RemoveBook("Third Book");

            // Assert
            Assert.Equal(2, bookStore.Count);
            Assert.Equal("First Book", bookStore[0].Title);
            Assert.Equal("Second Book", bookStore[1].Title);
        }

        [Fact]
        public void RemoveBook_RemovingMiddleBook_CorrectlyUpdatesStore()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("First Book", "Author", 2020, "Fiction");
            bookStore.AddBook("Second Book", "Author", 2021, "Fiction");
            bookStore.AddBook("Third Book", "Author", 2022, "Fiction");

            // Act
            bookStore.RemoveBook("Second Book");

            // Assert
            Assert.Equal(2, bookStore.Count);
            Assert.Equal("First Book", bookStore[0].Title);
            Assert.Equal("Third Book", bookStore[1].Title);
        }

        [Fact]
        public void RemoveBook_RemovingOnlyBook_ResultsInEmptyStore()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Only Book", "Author", 2020, "Fiction");

            // Act
            bookStore.RemoveBook("Only Book");

            // Assert
            Assert.Empty(bookStore);
        }

        [Fact]
        public void RemoveBook_RemovingFromEmptyStore_ThrowsInvalidOperationException()
        {
            // Arrange
            var bookStore = new BookStore();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                bookStore.RemoveBook("Any Book"));
            Assert.Contains("No book with the title", exception.Message);
        }

        [Fact]
        public void RemoveBook_MultipleRemoveOperations_WorksCorrectly()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book 1", "Author", 2020, "Fiction");
            bookStore.AddBook("Book 2", "Author", 2021, "Fiction");
            bookStore.AddBook("Book 3", "Author", 2022, "Fiction");
            bookStore.AddBook("Book 4", "Author", 2023, "Fiction");

            // Act
            bookStore.RemoveBook("Book 2");
            bookStore.RemoveBook("Book 4");

            // Assert
            Assert.Equal(2, bookStore.Count);
            Assert.Equal("Book 1", bookStore[0].Title);
            Assert.Equal("Book 3", bookStore[1].Title);
        }

        [Fact]
        public void RemoveBook_WithSpecialCharactersInTitle_RemovesCorrectBook()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("The C# Programming Language", "Author", 2020, "Technical");

            // Act
            bookStore.RemoveBook("The C# Programming Language");

            // Assert
            Assert.Empty(bookStore);
        }

        [Fact]
        public void RemoveBook_WithNumbers_RemovesCorrectBook()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("2001: A Space Odyssey", "Arthur C. Clarke", 1968, "Science Fiction");

            // Act
            bookStore.RemoveBook("2001: A Space Odyssey");

            // Assert
            Assert.Empty(bookStore);
        }

        [Fact]
        public void RemoveBook_PreservesOtherBookProperties()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Book to Remove", "Author", 2020, "Fiction");
            bookStore.AddBook("Book to Keep", "John Doe", 1995, "Mystery");

            // Act
            bookStore.RemoveBook("Book to Remove");

            // Assert
            Assert.Single(bookStore);
            var remainingBook = bookStore[0];
            Assert.Equal("Book to Keep", remainingBook.Title);
            Assert.Equal("John Doe", remainingBook.Author);
            Assert.Equal(1995, remainingBook.YearPublished);
            Assert.Equal("Mystery", remainingBook.Genre);
        }

        [Fact]
        public void RemoveBook_WithNullTitle_ThrowsInvalidOperationException()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Existing Book", "Author", 2020, "Fiction");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                bookStore.RemoveBook(null));
            Assert.Contains("No book with the title", exception.Message);
        }

        [Fact]
        public void RemoveBook_WithWhitespaceTitle_ThrowsInvalidOperationException()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Existing Book", "Author", 2020, "Fiction");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                bookStore.RemoveBook("   "));
            Assert.Contains("No book with the title", exception.Message);
        }

        [Fact]
        public void RemoveBook_RemoveAndAddSameTitle_WorksCorrectly()
        {
            // Arrange
            var bookStore = new BookStore();
            bookStore.AddBook("Test Book", "Author 1", 2020, "Fiction");

            // Act
            bookStore.RemoveBook("Test Book");
            bookStore.AddBook("Test Book", "Author 2", 2021, "Mystery");

            // Assert
            Assert.Single(bookStore);
            var book = bookStore[0];
            Assert.Equal("Test Book", book.Title);
            Assert.Equal("Author 2", book.Author);
            Assert.Equal(2021, book.YearPublished);
            Assert.Equal("Mystery", book.Genre);
        }
    }
}
