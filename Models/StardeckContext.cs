using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Stardeck.Models;

public partial class StardeckContext : DbContext
{
    public StardeckContext()
    {
    }

    public StardeckContext(DbContextOptions<StardeckContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Avatar> Avatars { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Deck> Decks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Stardeck;Username=Admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "account_email_key").IsUnique();

            entity.HasIndex(e => e.Nickname, "account_nickname_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.Coins)
                .HasDefaultValueSql("20")
                .HasColumnName("coins");
            entity.Property(e => e.Config)
                .HasColumnType("json")
                .HasColumnName("config");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Nickname).HasColumnName("nickname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.AvatarNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Avatar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_avatar_fkey");

            entity.HasMany(d => d.Avatars).WithMany(p => p.IdAccounts)
                .UsingEntity<Dictionary<string, object>>(
                    "AvatarAccount",
                    r => r.HasOne<Avatar>().WithMany()
                        .HasForeignKey("Avatar")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("avatar_account_avatar_fkey"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("avatar_account_id_account_fkey"),
                    j =>
                    {
                        j.HasKey("IdAccount", "Avatar").HasName("avatar_account_pkey");
                        j.ToTable("avatar_account");
                        j.IndexerProperty<string>("IdAccount")
                            .HasMaxLength(14)
                            .IsFixedLength()
                            .HasColumnName("id_account");
                        j.IndexerProperty<long>("Avatar").HasColumnName("avatar");
                    });
        });

        modelBuilder.Entity<Avatar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("avatar_pkey");

            entity.ToTable("avatar");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("card_pkey");

            entity.ToTable("card", tb => tb.HasComment("0-basica\n\n1-normal\n\n2-rara\n\n3-Muy Rara\n\n4-Ultra Rara"));

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Ability)
                .HasColumnType("json")
                .HasColumnName("ability ");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Battlecost).HasColumnName("battlecost");
            entity.Property(e => e.Energy).HasColumnName("energy");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Deck>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("deck_pkey");

            entity.ToTable("deck");

            entity.Property(e => e.IdAccount)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id_account");
            entity.Property(e => e.Deck1)
                .HasColumnType("json")
                .HasColumnName("deck");

            entity.HasOne(d => d.IdAccountNavigation).WithOne(p => p.Deck)
                .HasForeignKey<Deck>(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deck_account_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
