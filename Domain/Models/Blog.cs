using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated {get; set;}
        public DateTime DateUpdated { get; set; }
        public string  FileTransfePath { get; set; }

        //[ForeignKey("Category")] //this specifies that the CategoryId is the foreign key representing the reference with name "Category"
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
