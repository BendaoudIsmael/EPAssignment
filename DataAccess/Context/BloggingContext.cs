using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    //it is the gateway to the database 
    //by applying INHERITANCE I will be able to use built-in methods that will allow me to use LINQ such as queries, adding data, deleting
    public class BloggingContext: IdentityDbContext<CustomUser>
    {
        public BloggingContext (DbContextOptions<BloggingContext> options): base(options) { }
        public DbSet<Blog> Blogs { get; set; } //table names plular but class names singular
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
