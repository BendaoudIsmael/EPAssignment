using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class FileTransferRepository : IFileTransferRepository
    {
        public void AddFile(FileTransfer t)
        {
            throw new NotImplementedException();
        }

        public FileTransfer GetFile(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FileTransfer> GetTransfer()
        {
            throw new NotImplementedException();
        }
    }
}
