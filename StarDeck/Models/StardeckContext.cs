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

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Constant> Constants { get; set; }

    public virtual DbSet<Deck> Decks { get; set; }

    public virtual DbSet<FavoriteDeck> FavoriteDecks { get; set; }

    public virtual DbSet<Gamelog> Gamelogs { get; set; }

    public virtual DbSet<Gameroom> Gamerooms { get; set; }

    public virtual DbSet<Planet> Planets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=stardeck.postgres.database.azure.com;Database=Stardeck;Username=StardeckAdmin;Password=CE4101.1");

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

            entity.ToTable("card", tb => tb.HasComment("0-basica\r\n\r\n1-normal\r\n\r\n2-rara\r\n\r\n3-Muy Rara\r\n\r\n4-Ultra Rara"));

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
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Energy).HasColumnName("energy");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Race).HasColumnName("race");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("deck_pkey");

            entity.ToTable("collection");

            entity.Property(e => e.IdAccount)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id_account");
            entity.Property(e => e.Collection1)
                .HasColumnType("character(14)[]")
                .HasColumnName("collection");

            entity.HasOne(d => d.IdAccountNavigation).WithOne(p => p.Collection)
                .HasForeignKey<Collection>(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deck_account_fkey");
        });

        modelBuilder.Entity<Constant>(entity =>
        {
            entity
                .HasKey(e => e.Key);
            entity.ToTable("constants");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Deck>(entity =>
        {
            entity.HasKey(e => e.IdDeck).HasName("deck_pkey1");

            entity.ToTable("deck");

            entity.Property(e => e.IdDeck)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id_deck");
            entity.Property(e => e.Deck1)
                .HasColumnType("character(14)[]")
                .HasColumnName("deck");
            entity.Property(e => e.IdAccount)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id_account");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Decks)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deck_id_account_fkey");
        });

        modelBuilder.Entity<FavoriteDeck>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("favorite_deck_pkey");

            entity.ToTable("favorite_deck");

            entity.Property(e => e.Accountid)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("accountid");
            entity.Property(e => e.Deckid)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("deckid");

            entity.HasOne(d => d.Account).WithOne(p => p.FavoriteDeck)
                .HasForeignKey<FavoriteDeck>(d => d.Accountid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favorite_deck_accountid_fkey");

            entity.HasOne(d => d.Deck).WithMany(p => p.FavoriteDecks)
                .HasForeignKey(d => d.Deckid)
                .HasConstraintName("favorite_deck_deckid_fkey");
        });

        modelBuilder.Entity<Gamelog>(entity =>
        {
            entity.HasKey(e => e.Gameid).HasName("gamelog_pkey");

            entity.ToTable("gamelog");

            entity.Property(e => e.Gameid)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("gameid");
            entity.Property(e => e.Log).HasColumnName("log");

            entity.HasOne(d => d.Game).WithOne(p => p.Gamelog)
                .HasForeignKey<Gamelog>(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gamelog_gameid_fkey");
        });

        modelBuilder.Entity<Gameroom>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("gameroom_pkey");

            entity.ToTable("gameroom");

            entity.Property(e => e.Roomid)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("roomid");
            entity.Property(e => e.Bet).HasColumnName("bet");
            entity.Property(e => e.Player1)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("player1");
            entity.Property(e => e.Player2)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("player2");
            entity.Property(e => e.Winner)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("winner");

            entity.HasOne(d => d.Player1Navigation).WithMany(p => p.GameroomPlayer1Navigations)
                .HasForeignKey(d => d.Player1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gameroom_player1_fkey");

            entity.HasOne(d => d.Player2Navigation).WithMany(p => p.GameroomPlayer2Navigations)
                .HasForeignKey(d => d.Player2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gameroom_player2_fkey");

            entity.HasOne(d => d.WinnerNavigation).WithMany(p => p.GameroomWinnerNavigations)
                .HasForeignKey(d => d.Winner)
                .HasConstraintName("gameroom_winner_fkey");
        });

        modelBuilder.Entity<Planet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planet_pkey");

            entity.ToTable("planet", tb => tb.HasComment("0 raro\n1 basico\n2 popular"));

            entity.HasIndex(e => e.Name, "planet_name_name1_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Ability)
                .HasColumnType("json")
                .HasColumnName("ability");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
