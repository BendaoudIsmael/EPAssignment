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

        public void AddFileTransfer(FileCreationModel model)
        {
            throw new NotImplementedException();
        }

        public FileTransferModel GetFiles(int id)
        {
            FileTransferModel model = new FileTransferModel();
            var file = fileTransferRepo.GetFile(id);
            model.ReceiverEmail = file.EmailReciver;
            model.SenderEmail = file.EmailSent;
            model.FilePath = file.FilePath;
            model.Id = file.Id;
            model.Title = file.Title;

            return model;
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
