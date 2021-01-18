using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        [Route("Book")]
        public IActionResult List(int? authorId, int? borrowerId)
        {
            if (authorId == null && borrowerId == null)
            {
                var books = bookRepository.GetAllWithAuthor();
                return CheckBooks(books);
            }
            else if (authorId != null)
            {
                var author = authorRepository
                    .GetWithBooks((int)authorId);
                if (author.Books.Count() == 0)
                    return View("AuthorEmpty", author);
                else
                {
                    return View(author.Books);
                }
            }
            else if (borrowerId != null)
            {
                var books = bookRepository
                    .FindWithAuthorAndBorrower(book => book.BorrowerId == borrowerId);
                return CheckBooks(books);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public IActionResult CheckBooks(IEnumerable<Book> books)
        {
            if (books.Count() == 0)
                return View("Empty");
            else
            {
                return View(books);
            }
        }

        public IActionResult Create()
        {
            var book = new BookViewModel()
            {
                Authors = authorRepository.GetAll()
            };
            return View(book);
        }

        [HttpPost]
        public IActionResult Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                bookViewModel.Authors = authorRepository.GetAll();
                return View(bookViewModel);
            }
            bookRepository.Create(bookViewModel.Book);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var book = new BookViewModel()
            {
                Book = bookRepository.GetById(id),
                Authors = authorRepository.GetAll()
            };
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                bookViewModel.Authors = authorRepository.GetAll();
                return View(bookViewModel);
            }
            bookRepository.Update(bookViewModel.Book);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var book = bookRepository.GetById(id);
            bookRepository.Delete(book);
            return RedirectToAction("List");
        }
    }
}
