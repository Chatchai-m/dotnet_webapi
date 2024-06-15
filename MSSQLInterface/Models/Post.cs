using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLInterface.Models
{
    public class Post
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool Archived { get; set; }

        public Int64 BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
