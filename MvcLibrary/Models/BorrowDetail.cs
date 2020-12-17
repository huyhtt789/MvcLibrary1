using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class BorrowDetail
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
//=============================================================
        public int IdBook { get; set; }
        public int IdBorrowing { get; set; }
//=============================================================

        [ForeignKey("IdBook")]
        public virtual Book Book { get; set; }        
        [ForeignKey("IdBorrowing")]
        public virtual Borrowing Borrowing { get; set; }


    }
}
