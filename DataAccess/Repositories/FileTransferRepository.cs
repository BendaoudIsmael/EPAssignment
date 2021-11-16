using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    class FileTransferRepository : IFileTransferRepository
    {
        public BloggingContext context { get; set; }
        public IFileTransferRepository(BloggingContext _context)
        {
            context = _context;
        }
        public void AddBlog(Blog b)
        {
            b.DateCreated = DateTime.Now;
            context.Blogs.Add(b);
            context.SaveChanges();
        }

        public void DeleteBlog(Blog b)
        {
            context.Blogs.Remove(b);
            context.SaveChanges();
        }

        public Blog GetBlog(int id)
        {
            //lambda expression

            //take x as an argument which will represent all the Blogs in the database
            return context.Blogs.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Blog> GetBlogs()
        {
            return context.Blogs;
        }

        public void UpdateBlog(Blog b)
        {
            Blog originalBlog = GetBlog(b.Id);
            originalBlog.DateCreated = b.DateCreated;
            originalBlog.DateUpdated = DateTime.Now;
            originalBlog.FileTransfePath = b.FileTransfePath;
            originalBlog.Email = b.Email;
        }
    }
}
