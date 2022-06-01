using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eLearningAutomotiveWebSite.Models;

namespace eLearningAutomotiveWebSite.Data
{
    public class eLearningAutomotiveWebSiteContext : DbContext
    {
        public eLearningAutomotiveWebSiteContext (DbContextOptions<eLearningAutomotiveWebSiteContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Content> Content { get; set; }

        public DbSet<History> History { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<User> User { get; set; }
    }
}
