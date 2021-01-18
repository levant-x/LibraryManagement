using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        { }

        public IEnumerable<Book> FindWithAuthor(Func<Book, bool> predicate)
        {
            return context.Books
                .Include(book => book.Author)
                .Where(predicate);
        }

        public IEnumerable<Book> FindWithAuthorAndBorrower(Func<Book, bool> predicate)
        {
            return context.Books
                .Include(book => book.Author)
                .Include(book => book.Borrower)
                .Where(predicate);
        }

        public IEnumerable<Book> GetAllWithAuthor()
        {
            return context.Books.Include(book => book.Author);
        }
    }
}
