using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLInterface.Models
{
    public class Blog
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string SiteUri { get; set; }

        public ICollection<Post> Posts { get; } 
    }
}
