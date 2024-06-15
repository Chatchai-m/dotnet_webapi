using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLInterface.Data
{
    public class MSSQLSchemaContext : MSSQLContext
    {
        public MSSQLSchemaContext()
        {
            Console.WriteLine("<================= MSSQLSchemaContext =================>");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connection_string = this.app_dev_schema;
            // connect to sqlite database
            options.UseSqlServer(connection_string);
        }
    }
}
