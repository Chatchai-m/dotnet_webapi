using Microsoft.EntityFrameworkCore;
using MSSQLInterface.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLInterface.Data
{
    public class MSSQLContext : DbContext
    {
        //protected string app_pro = "Data Source=tdaa-dataissue-dbserver.database.windows.net; Initial Catalog=test_aa; User Id=tdaa; Password=dataissue2024?;";
        protected string app_test = "";
        protected string app_dev = "";
        //protected string app_dev_schema = "Data Source=tdaa-dataissue-dbserver.database.windows.net; Initial Catalog=test_aa; User Id=tdaa; Password=dataissue2024?;";

        protected string app_pro = "Server=localhost;Database=dotnet_webapi; User Id=mos; Password=123456; TrustServerCertificate=True;";
        protected string app_dev_schema = "Server=localhost;Database=dotnet_webapi_schema; User Id=mos; Password=123456; TrustServerCertificate=True;";
        public MSSQLContext()
        {
            this.Database.SetCommandTimeout(600);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connection_string = this.app_pro;
            options.UseSqlServer(connection_string);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ToTable("blog");
            modelBuilder.Entity<Post>().ToTable("post");

            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.Blog)
                .HasForeignKey(e => e.BlogId)
                .HasPrincipalKey(e => e.Id);
        }

    }
}
