using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;

        public ReturnController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
        }

        [Route("Return")]
        public IActionResult List()
        {
            var borrowedBooks = bookRepository.FindWithAuthorAndBorrower(borrowed 
                => borrowed.BorrowerId != 0);
            if(borrowedBooks == null || borrowedBooks.ToList().Count() == 0)
                return View("Empty");
            return View(borrowedBooks);
        }

        public IActionResult ReturnABook(int bookId)
        {
            var book = bookRepository.GetById(bookId);
            book.Borrower = null;
            book.BorrowerId = 0;
            bookRepository.Update(book);
            return RedirectToAction("List");
        }
    }
}