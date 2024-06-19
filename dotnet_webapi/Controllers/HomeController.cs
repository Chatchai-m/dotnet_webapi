using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Emit;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Context;
using Serilog;
using MSSQLInterface.Data;
using MSSQLInterface.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace dotnet_webapi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            using (LogContext.PushProperty("type1", true))
            {
                // Process request; all logged events will carry `event_type`
                //_logger.LogInformation("1. Type_1");
                //_logger.LogWarning("2. Warning");
                //_logger.LogError("3. Error");
                //_logger.LogCritical("4. Critical");
                Log.Information("1.This log message will have an event_type property");
                Log.Warning("2.ABCD");
                Log.Error("3.ABCD");
                Log.Fatal("4.ABCD");
            }
            return Ok("Hello world -> 1");
        }

        public IActionResult Index2()
        {
            using (LogContext.PushProperty("type2", true))
            {
                // Process request; all logged events will carry `event_type`
                Log.Information("1.type2");
                Log.Warning("2.ABCD");
                Log.Error("3.ABCD");
                Log.Fatal("4.ABCD");
            }
            return Ok("Hello world -> 2");
        }


        public IActionResult LoadBlog()
        {
            MSSQLContext mSSQLContext = new MSSQLContext();

            var blogs1 = (
                from q_blog in mSSQLContext.Blog
                select new
                {
                    q_blog.Id,
                    q_blog.Name,
                    posts = q_blog.Posts.Select( q => new {q.Id, q.Title, q.Content } ).ToList() //q_blog.Posts
                }
            ).ToList();

            var blogs2 = ( from q_blog2 in mSSQLContext.Blog select q_blog2 ).ToList();
            //var blogs2 = (from q_post in mSSQLContext.Post select q_post ).ToList();

            Dictionary<string, dynamic> rs = new Dictionary<string, dynamic>();

            rs["blogs1"] = blogs1;
            rs["blogs2"] = blogs2;
            return Json(new { rs });
        }

        public string GetUniqueKeyOriginal_BIASED(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public IActionResult SaveBlog()
        {
            string Name = this.GetUniqueKeyOriginal_BIASED(30);
            string SiteUri = this.GetUniqueKeyOriginal_BIASED(50);
            MSSQLContext mSSQLContext = new MSSQLContext();
            Blog blog = new Blog();
            blog.Name = Name;
            blog.SiteUri = SiteUri;
            mSSQLContext.Blog.Add(blog);
            mSSQLContext.SaveChanges();
            return Ok("Save Blog");
        }

        public IActionResult SavePost()
        {
            string Title = this.GetUniqueKeyOriginal_BIASED(10);
            string Content = this.GetUniqueKeyOriginal_BIASED(150);
            MSSQLContext mSSQLContext = new MSSQLContext();
            Post post = new Post();
            post.Title = Title;
            post.Content = Content;
            post.PublishedOn = DateTime.Now;
            post.Archived = true;
            post.BlogId = 1;
            mSSQLContext.Add(post);
            mSSQLContext.SaveChanges();
            return Ok("Save Post");
        }
    }
}