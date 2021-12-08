using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class FileTransferModel
    {
        public int Id { get; set; }
        public string ReceiverEmail { get; set; }
        public string SenderEmail { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
    }
}