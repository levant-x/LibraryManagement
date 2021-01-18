using LibraryManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        { }

        public IEnumerable<Author> GetAllWithBooks()
        {
            return context.Authors.Include(book => book.Books);
        }

        public Author GetWithBooks(int id)
        {
            return context.Authors
                .Where(author => author.AuthorId == id)
                .Include(author => author.Books)
                .FirstOrDefault();
        }
    }
}
