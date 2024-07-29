using Microsoft.EntityFrameworkCore;
using Raga.Server.Data.Configurations;
using Raga.Server.Data.Models;

namespace Raga.Server.Data;

public class GachaContext(
    DbContextOptions<GachaContext> options)
    : DbContext(options)
{
    public DbSet<GachaItem> GachaItems { get; set; }
    public DbSet<PlayerStats> PlayerStats { get; set; }
    public DbSet<Clan> Clans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GachaItemConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerStatsConfiguration());
        modelBuilder.ApplyConfiguration(new ClanConfiguration());
    }
}