using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class FileContext: DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options) { }
        public DbSet<File> Files { get; set; }
    }
}
