using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
//==================================================================
        public virtual ICollection<BookGenre> BookGenres { get; set; }
//        public virtual ICollection<Book> Books { get; set; }

    }
}
