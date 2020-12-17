using MvcLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<BookGenre> BookGenres { get; set; }    
        public String PageTitle { get; set; }

    }

    public class BookIn4
    {
        private readonly List<BookGenre> bookGenresList;

        public BookIn4()
        {
            bookGenresList = new List<BookGenre>();
        }

        public IEnumerable<BookGenre> GetListBookGenres()
        {
            return bookGenresList;
        }
    }
}
