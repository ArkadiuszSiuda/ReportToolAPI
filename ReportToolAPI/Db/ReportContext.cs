using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReportToolAPI.Entities;
using ReportToolAPI.Interfaces;
using System.Runtime.CompilerServices;

namespace ReportToolAPI.Db;

public class ReportContext : ApiAuthorizationDbContext<IdentityUser>
{
    private readonly ICurrentUserService _currentUserService;

    public DbSet<Code> Codes => Set<Code>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Report> Reports => Set<Report>();

    public ReportContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationOptions,
        ICurrentUserService currentUserService) : base(options, operationOptions)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>().HasQueryFilter(r => r.OwnerId == Guid.Parse(_currentUserService.UserId!));
        base.OnModelCreating(modelBuilder);
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
        foreach (var entry in ChangeTracker.Entries<OwnedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.OwnerId = Guid.Parse(_currentUserService.UserId!);
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}