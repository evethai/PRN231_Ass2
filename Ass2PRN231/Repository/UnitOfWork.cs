using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{

    public class UnitOfWork : IDisposable
    {
        private Ass2Prn231Context _context = new Ass2Prn231Context();
        private GenericRepository<Book> _bookRepository;
        private GenericRepository<Author> _authorRepository;
        private GenericRepository<BookAuthor> _bookAuthorRepository;
        private GenericRepository<Publisher> _publisherRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<User> _userRepository;

        public GenericRepository<Book> BookRepository
        {
            get
            {
                if (this._bookRepository == null)
                {
                    this._bookRepository = new GenericRepository<Book>(_context);
                }
                return _bookRepository;
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {
                if (this._authorRepository == null)
                {
                    this._authorRepository = new GenericRepository<Author>(_context);
                }
                return _authorRepository;
            }
        }

        public GenericRepository<BookAuthor> BookAuthorRepository
        {
            get
            {
                if (this._bookAuthorRepository == null)
                {
                    this._bookAuthorRepository = new GenericRepository<BookAuthor>(_context);
                }
                return _bookAuthorRepository;
            }
        }
        public GenericRepository<Publisher> PublisherRepository
        {
            get
            {
                if (this._publisherRepository == null)
                {
                    this._publisherRepository = new GenericRepository<Publisher>(_context);
                }
                return _publisherRepository;
            }
        }
        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                {
                    this._roleRepository = new GenericRepository<Role>(_context);
                }
                return _roleRepository;
            }
        }
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;
            var invokedExpr = Expression.Invoke(right, left.Parameters);
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }
    }


}
