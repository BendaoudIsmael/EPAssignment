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

        public void AddFileTransfer(FileTransferModel t) 
        {
            fileTransferRepo.AddFile(
                new Domain.Models.File()
                {
                    Id = t.Id,
                    EmailReciver = t.ReceiverEmail,
                    EmailSent = t.SenderEmail,
                    Title = t.Title,
                    FilePath = t.FilePath
                });
        }

        public FileTransferModel GetFile(int id)
        {
            FileTransferModel myModel = new FileTransferModel();
            var file = fileTransferRepo.GetFile(id);
            myModel.ReceiverEmail = file.EmailReciver;
            myModel.SenderEmail = file.EmailSent;
            myModel.FilePath = file.FilePath;
            myModel.Id = file.Id;
            myModel.Title = file.Title;

            return myModel;
        }

        public IQueryable<FileTransferModel> GetFiles()
        {
            var list = fileTransferRepo.GetTransfer();

            List<FileTransferModel> myResults = new List<FileTransferModel>();

            foreach(var t in list)
            {
                myResults.Add(new FileTransferModel()
                {
                    ReceiverEmail = t.EmailReciver,
                    SenderEmail = t.EmailSent,
                    FilePath = t.FilePath,
                    Id = t.Id,
                    Title = t.Title
                });
            }

            return myResults.AsQueryable();
        }
    }
}
