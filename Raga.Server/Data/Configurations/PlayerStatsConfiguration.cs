using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raga.Server.Data.Models;

namespace Raga.Server.Data.Configurations;

public class PlayerStatsConfiguration : IEntityTypeConfiguration<PlayerStats>
{
    public void Configure(EntityTypeBuilder<PlayerStats> builder)
    {
        builder.HasKey(p => p.PlayerId);
        builder.Property(p => p.Level).IsRequired();
        builder.Property(p => p.TotalPulls).IsRequired();
        builder.Property(p => p.TotalCurrency).IsRequired();
        builder.Property(p => p.LastDailyRewardClaim).IsRequired();
        builder.Property(p => p.Achievements).HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        );
    }
}