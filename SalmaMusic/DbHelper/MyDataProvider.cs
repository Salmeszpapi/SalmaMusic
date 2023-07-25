using Microsoft.EntityFrameworkCore;
using SalmaMusic.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.DbHelper
{
    class MyDataProvider : DbContext
    {
        public DbSet<Music> Music { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Directory.GetParent(appDirectory)?.Parent?.Parent?.Parent?.FullName;

            optionsBuilder.UseSqlite($"Data Source={parentDirectory}\\MyDatabase.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Music>();
        }
    }
}
