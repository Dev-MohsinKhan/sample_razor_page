using Microsoft.EntityFrameworkCore;
using SampleRazorPage.Pages;
using System.Collections.Generic;

namespace SampleRazorPage
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
