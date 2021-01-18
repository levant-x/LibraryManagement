﻿using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [Route("Author")]
        public IActionResult List()
        {
            var authors = authorRepository.GetAllWithBooks();
            if (authors.Count() == 0)
                return View("Empty");
            return View(authors);
        }

        public IActionResult Update(int id)
        {
            var author = authorRepository.GetById(id);
            if (author == null)
                return NotFound();
            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);
            authorRepository.Update(author);
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            var viewModel = new CreateAuthorViewModel
            {
                Referer = $"{Request.Headers["Referer"]}"
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateAuthorViewModel author)
        {
            if (!ModelState.IsValid)
                return View(author);
            authorRepository.Create(author.Author);
            if (!String.IsNullOrEmpty(author.Referer))
                return Redirect(author.Referer);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var author = authorRepository.GetById(id);
            authorRepository.Delete(author);
            return RedirectToAction("List");
        }
    }
}
