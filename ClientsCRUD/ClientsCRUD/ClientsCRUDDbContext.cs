using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClientsCRUD.Models;

public class ClientsCRUDDbContext : IdentityDbContext<IdentityUser>
{
    public ClientsCRUDDbContext(DbContextOptions<ClientsCRUDDbContext> options)
            : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Account> Accounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure precision and scale for the Balance property
        modelBuilder.Entity<Account>()
            .Property(a => a.Balance)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Client>()
         .HasOne(c => c.Address)
         .WithOne()
         .HasForeignKey<Address>(a => a.ClientId);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Accounts)
            .WithOne()
            .HasForeignKey(a => a.ClientId);

    }
}