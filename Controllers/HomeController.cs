using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.ViewModel;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository bookRepository;
        private IAuthorRepository authorRepository;
        private ICustomerRepository customerRepository;

        public HomeController(IBookRepository bookRepository, IAuthorRepository authorRepository,
            ICustomerRepository customerRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            var home = new HomeViewModel()
            {
                AuthorCount = authorRepository.Count(author => true),
                CustomerCount = customerRepository.Count(customer => true),
                BookCount = bookRepository.Count(book => true),
                LendBookCount = bookRepository.Count(borrowed => borrowed.Borrower != null)
            };
            return View(home);
        }
    }
}
