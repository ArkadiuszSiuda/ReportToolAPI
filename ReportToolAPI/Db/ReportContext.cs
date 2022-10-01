using Microsoft.EntityFrameworkCore;
using ReportToolAPI.Entities;

namespace ReportToolAPI.Db;

public class ReportContext : DbContext
{
    public DbSet<Code> Codes => Set<Code>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Report> Reports => Set<Report>();

    public ReportContext(DbContextOptions options) : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.Modified = DateTime.Now;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}