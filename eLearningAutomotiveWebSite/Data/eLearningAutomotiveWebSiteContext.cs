using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eLearningAutomotiveWebSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eLearningAutomotiveWebSite.Data
{
    public class eLearningAutomotiveWebSiteContext : IdentityDbContext
    {
        public eLearningAutomotiveWebSiteContext(DbContextOptions<eLearningAutomotiveWebSiteContext> options)
            : base(options)
        {
        }

        public DbSet<eLearningAutomotiveWebSite.Models.Category>? Category { get; set; }

        public DbSet<eLearningAutomotiveWebSite.Models.Content>? Content { get; set; }

        public DbSet<eLearningAutomotiveWebSite.Models.History>? History { get; set; }

    }
}
