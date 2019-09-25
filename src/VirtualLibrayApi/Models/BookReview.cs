using System;

namespace VirtualLibraryApi.Models
{
    public class BookReview
    {
        public long BookId { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}