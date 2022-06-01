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

        public DbSet<eLearningAutomotiveWebSite.Models.Category>? Category { get; set; }

        public DbSet<eLearningAutomotiveWebSite.Models.Content>? Content { get; set; }

        public DbSet<eLearningAutomotiveWebSite.Models.History>? History { get; set; }

        public DbSet<eLearningAutomotiveWebSite.Models.Role>? Role { get; set; }

        public DbSet<eLearningAutomotiveWebSite.Models.User>? User { get; set; }
    }
}
