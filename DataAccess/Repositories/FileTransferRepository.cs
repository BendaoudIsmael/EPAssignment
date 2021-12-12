using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FileTransferRepository : IFileTransferRepository
    {
        public FileTransferContext context { get; set; }
        public FileTransferRepository(FileTransferContext _context)
        {
            context = _context;
        }

        public void AddFile(File t)
        {
            context.Files.Add(t);
            context.SaveChanges();
        }

        public File GetFile(int id)
        {
            return context.Files.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<File> GetTransfer()
        {
            return context.Files;
        }
    }
}
