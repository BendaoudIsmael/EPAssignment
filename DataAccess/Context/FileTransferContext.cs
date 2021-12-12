using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class FileTransferContext: DbContext
    {
        public FileTransferContext(DbContextOptions<FileTransferContext> options) : base(options) { }
        public DbSet<File> Files { get; set; }
    }
}
