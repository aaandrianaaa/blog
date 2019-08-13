using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Models
{
    public class BlogContext : DbContext
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Confirmation> Confirmations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SavedArticles> SavedArticles { get; set; }



        public BlogContext(DbContextOptions<BlogContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

    }

}
