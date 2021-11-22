using Application.Interfaces;
using Application.ViewModels;
using DataAccess.Repositories;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class FileTransferService : IFileTransferService
    {
        private IFileTransferRepository fileTransferRepo;
        public FileTransferService(IFileTransferRepository _fileTransferRepo)
        {
            fileTransferRepo = _fileTransferRepo;
        }
        public void AddFileTransfer(FileTransferModel t) { 
        
        }

        public void GetFileTranfer() {
        
        }
    }
}
