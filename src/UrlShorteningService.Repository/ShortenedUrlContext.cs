using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShorteningService.Repository.Models;

namespace UrlShorteningService.Repository
{
    public class ShortenedUrlContext : DbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public string DbPath { get; }

        public ShortenedUrlContext()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var dbDirectory = $"{Directory.GetParent(currentDirectory).Parent}\\db";
            DbPath = System.IO.Path.Join(dbDirectory, "ShortenedUrls.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
