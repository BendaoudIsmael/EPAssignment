using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IFileTransferService
    {
        public IQueryable<FileTransferModel> GetFileTransfer();
        public void AddFileTransfer(FileTransferModel t);
    }
}