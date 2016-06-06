using System;

namespace BlogDomainProject.DTO
{
    public class BlogSummary
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfViews { get; set; }
        public DateTime DatePosted { get; set; }
        public int BlogId { get; set; }
    } 
}
