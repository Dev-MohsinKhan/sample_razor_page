using Microsoft.EntityFrameworkCore;
using SampleRazorPage.Pages;
using System.Collections.Generic;
using static SampleRazorPage.Models.PocoModel;

namespace SampleRazorPage
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Region> Regions { get; set; }
        public DbSet<Wiliyat> Wiliyats { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<EntityType> EntityType { get; set; }
        public DbSet<Item> Item { get; set; }
    }
}
