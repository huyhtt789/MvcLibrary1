using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class Librarian
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }

        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; }
//============================================================
//        public int IdBorrowing { get; set; }
//============================================================
//        [ForeignKey("IdBorrowing")]
        public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}
