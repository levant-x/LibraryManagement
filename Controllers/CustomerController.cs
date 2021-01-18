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
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IBookRepository bookRepository;

        public CustomerController(ICustomerRepository customerRepository, IBookRepository bookRepository)
        {
            this.customerRepository = customerRepository;
            this.bookRepository = bookRepository;
        }

        [Route("Customer")]
        public IActionResult List()
        {
            var customersList = new List<CustomerViewModel>();
            var customers = customerRepository.GetAll();
            if (customers.Count() == 0)
                return View("Empty");
            foreach (var customer in customers)
                customersList.Add(new CustomerViewModel
                {
                    Customer = customer,
                    BookCount = bookRepository.Count(book => book.BorrowerId == customer.CustomerId)
                });
            return View(customersList);
        }

        public IActionResult Delete(int id)
        {
            var customer = customerRepository.GetById(id);
            customerRepository.Delete(customer);
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);
            customerRepository.Create(customer);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var customer = customerRepository.GetById(id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);
            customerRepository.Update(customer);
            return RedirectToAction("List");
        }
    }
}
