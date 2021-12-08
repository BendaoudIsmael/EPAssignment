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
        public IQueryable<FileTransferModel> GetFiles();
        public void AddFileTransfer(FileTransferModel t);

    }
}