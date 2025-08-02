using AutoPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoPortfolio.Data
{
    public class AppDbContext : DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }


    public DbSet<Repository> Repositories {get; set;}
    }
    
    
}
