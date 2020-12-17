using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Placement { get; set; }
        public ushort Stock { get; set; }

        public string ImgUrl { get; set; }
        //============================================================================
        public int IdPublishingCompany { get; set; }

//============================================================================
 //       public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<BorrowDetail> BorrowDetails { get; set; }
        [ForeignKey("IdPublishingCompany")]
        public virtual PublishingCompany PublishingCompany { get; set; }

        public virtual ICollection<BookGenre> BookGenre { get; set; }
        //============================================================================
        /*
        
        */
    }
}
