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
        public FileContext context { get; set; }
        public FileTransferRepository(FileContext _context)
        {
            context = _context;
        }

        public void AddFile(File t)
        {
            throw new NotImplementedException();
        }

        public File GetFile(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<File> GetTransfer()
        {
            throw new NotImplementedException();
        }
    }
}
