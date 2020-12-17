using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class BookGenre
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public int IdGenre { get; set; }
        [ForeignKey("IdBook")]
        public virtual Book Book { get; set; }
        [ForeignKey("IdGenre")]
        public virtual Genre Genre { get; set; }
    }
}
