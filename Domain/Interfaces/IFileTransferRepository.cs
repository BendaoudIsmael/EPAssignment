using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IFileTransferRepository
    {
        public IQueryable<FileTransfer> GetFiles();
        public void AddFile(FileTransfer t);
        public FileTransfer GetFile(int id);

    }
}
