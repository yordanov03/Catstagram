using Catstagram.Server.Data.Models;
using Catstagram.Server.Data.Models.Base;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Data
{
    public class CatstagramDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUserService;
        public CatstagramDbContext(DbContextOptions<CatstagramDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<Cat> Cats { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cat>()
                .HasQueryFilter(c=>!c.IsDeleted)
                .HasOne(c => c.User)
                .WithMany(u => u.Cats)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<Profile>(p=>p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInformation()
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var username = this.currentUserService.GetUserName();

                     if (entry.Entity is IDeletableEntity deletableEntity)
                    {
                        if(entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.DeletedBy = username;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;

                            return;
                        }
                    }

                    if (entry.Entity is IEntity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = username;
                        }
                        else if(entry.State == EntityState.Modified)
                        {
                            entity.ModifiedOn = DateTime.UtcNow;
                            entity.ModifiedBy = username;
                        }
                    }
                });
        }
    }
}