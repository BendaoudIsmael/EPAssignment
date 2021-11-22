using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IFileTransferRepository
    {
        public IQueryable<FileTransfer> GetTransfer();
        public void AddFile(FileTransfer t);
        public FileTransfer GetFile(int id);

    }
}
