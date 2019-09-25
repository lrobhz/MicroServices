using System.Collections.Generic;
using VirtualLibraryApi.Models;
using VirtualLibrayApi.Models;

namespace src.Models
{
    public class Basket
    {
        public User User { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}