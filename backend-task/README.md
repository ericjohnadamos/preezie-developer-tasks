### Take-Home Exercise: Library Management System API

#### Objective:
Develop a simple Library Management System API in C#. This task will allow you to showcase your familiarity with design patterns, code organization, and best practices.

#### Description:
Create a C# Web API that simulates a Library Management System. The system should allow adding, borrowing, and returning books. Utilize appropriate design patterns where applicable (e.g., Factory, Repository, etc.).

#### Requirements:
1. **Book Management:**
   - Create a `Book` class with properties: `Id`, `Title`, `Author`, `ISBN`, and `IsBorrowed`.
   - Implement functionality to add new books to the library.

2. **Library Operations:**
   - Create a `Library` class that manages the collection of books.
   - Implement methods for:
     - Adding a book to the library.
     - Borrowing a book by its `Id`.
     - Returning a book by its `Id`.
     - Listing all books in the library (indicating if they are borrowed or not).

3. **API Endpoints:**
   - Create endpoints to:
     - Add a new book (`POST /api/books`).
     - Borrow a book (`PUT /api/books/{id}/borrow`).
     - Return a book (`PUT /api/books/{id}/return`).
     - List all books (`GET /api/books`).

4. **Design Patterns:**
   - Use the Singleton pattern to ensure there is only one instance of the `Library`.
   - Optionally, use the Repository pattern to manage data access (in-memory for this exercise).
   - Bonus points: Refactor the code using Clean Architecture.

#### Additional Guidelines:
- Ensure to handle edge cases (e.g., borrowing a non-existent book, returning a book that isn't borrowed).
- Use meaningful variable and method names.
- Comment your code where necessary to explain design decisions.

#### Skeleton Code:
You may start with the following skeleton code to guide your implementation. <br/>
***Note***: The code is not optimised (e.g., it does not use dependency injection). Your task is also to identify potential improvements and refactor where possible.

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }
    }

    // Singleton Pattern for Library
    public class Library
    {
        private static Library _instance;
        private List<Book> books;

        private Library()
        {
            books = new List<Book>();
        }

        public static Library Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Library();
                }
                return _instance;
            }
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public bool BorrowBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                return true;
            }
            return false;
        }

        public bool ReturnBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                return true;
            }
            return false;
        }

        public List<Book> ListBooks()
        {
            return books;
        }
    }

    // Factory Pattern for Book creation
    public static class BookFactory
    {
        private static int _nextId = 1;

        public static Book CreateBook(string title, string author, string isbn)
        {
            return new Book
            {
                Id = _nextId++,
                Title = title,
                Author = author,
                ISBN = isbn,
                IsBorrowed = false
            };
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly Library _library;

        public BooksController()
        {
            _library = Library.Instance;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            var newBook = BookFactory.CreateBook(book.Title, book.Author, book.ISBN);
            _library.AddBook(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}/borrow")]
        public IActionResult BorrowBook(int id)
        {
            if (_library.BorrowBook(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}/return")]
        public IActionResult ReturnBook(int id)
        {
            if (_library.ReturnBook(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ListBooks()
        {
            return Ok(_library.ListBooks());
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _library.ListBooks().FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
```

#### Submission Instructions:
1. Create a new repository on your personal GitHub account.
2. Commit your changes and ensure your code is well-documented.
3. Share the repository link once you are done.

Good luck and happy coding!
