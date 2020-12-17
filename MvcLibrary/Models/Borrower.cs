using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Respect { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpireDate { get; set; }
//================================================================
        //public int IdBorrowing  { get; set; }

//================================================================
        //[ForeignKey("IdBorrowing")]
        public virtual ICollection<Borrowing> Borrowings { get; set; }

    }
}
