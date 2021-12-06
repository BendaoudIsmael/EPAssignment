using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IFileTransferRepository
    {
        public IQueryable<File> GetTransfer();
        public void AddFileTransfer(File t);
        public File GetFile(int id);

    }
}
