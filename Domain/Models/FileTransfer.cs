using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FileTransfer
    {
        public int Id { get; set; }
        public string EmailReciver { get; set; }
        public string EmailSent { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        
    }
}
