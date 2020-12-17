using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class Borrowing
    {
        public int Id { get; set; } 

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
//============================================================================        
        public int IdBorrower  { get; set; }
        public int IdLibrarian { get; set; }

//============================================================================
        [ForeignKey("IdBorrower")]
        public virtual Borrower Borrower { get; set; }
        [ForeignKey("IdLibrarian")]
        public virtual Librarian Librarian { get; set; }

        public virtual ICollection<BorrowDetail> BorrowDetails { get; set; }


    }
}

/*
 * 
 *  ALTER TABLE Borrowing

    ADD CONSTRAINT FK_Borrowing
    FOREIGN KEY IdBorrower REFERENCES Borrower
    ON UPDATE CASCADE
    ON DELETE CASCADE,

    ADD CONSTRAINT FK_Borrower
    FOREIGN KEY IdBorrower REFERENCES Borrower
    ON UPDATE CASCADE
    ON DELETE CASCADE,

    ADD CONSTRAINT FK_IdBorrower
    FOREIGN KEY IdBorrower REFERENCES Borrower
    ON UPDATE CASCADE
    ON DELETE CASCADE
*/