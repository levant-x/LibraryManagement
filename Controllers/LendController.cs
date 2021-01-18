using LibraryManagement.Data.Interfaces;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class LendController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;

        public LendController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
        }

        [Route("Lend")]
        public IActionResult List()
        {
            var availableBooks = bookRepository.FindWithAuthor(book => book.BorrowerId == 0);
            if (availableBooks.Count() == 0)
                return View("Empty");
            else
            {
                return View(availableBooks);
            }
        }

        public IActionResult LendBook(int bookId)
        {
            var borrowing = new LendViewModel()
            {
                Book = bookRepository.GetById(bookId),
                Customers = customerRepository.GetAll()
            };
            return View(borrowing);
        }

        [HttpPost]
        public IActionResult LendBook(LendViewModel lendViewModel)
        {
            var book = bookRepository.GetById(lendViewModel.Book.BookId);
            var customer = customerRepository.GetById(lendViewModel.Book.BorrowerId);
            book.Borrower = customer;
            bookRepository.Update(book);
            return RedirectToAction("List");
        }
    }
}
