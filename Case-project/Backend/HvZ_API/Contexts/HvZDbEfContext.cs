using Microsoft.EntityFrameworkCore;
using HvZ_API.Models;

namespace HvZ_API.Contexts
{
    public class HvZDbEfContext : DbContext
    {
        public HvZDbEfContext(DbContextOptions options) : base(options)
        {
        }
        protected HvZDbEfContext()
        {
        }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameConfig> GameConfig { get; set; }
        public DbSet<Gravestone> Gravestone { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Squad> Squad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Rank

            //GameConfig
            modelBuilder.Entity<GameConfig>().HasData(
                new GameConfig() { 
                    Id = 1, 
                    PlayerCount = 64, 
                    InitZombies = 2, 
                    Duration = 12, 
                    HungerDuration = 1, 
                    ChatCooldown = 20 
                },
                new GameConfig() { 
                    Id = 2, 
                    PlayerCount = 64, 
                    InitZombies = 2, 
                    Duration = 24, 
                    HungerDuration = 2, 
                    ChatCooldown = 0 
                }
                );
            //Game
            modelBuilder.Entity<Game>().HasData(
                new Game() { 
                    Id = 1, 
                    Name = "First Game",
                    Description = "First Desc", 
                    StartTime = DateTime.Now,
                    Is_Started = false, 
                    nw_Lat = 20, 
                    nw_Lng = 20, 
                    se_Lat = 20, 
                    se_Lng = 20, 
                    image = null, 
                    GameConfigId = 1 
                },

                new Game() { 
                    Id = 2, 
                    Name = "Second Game", 
                    Description = "Second Desc", 
                    StartTime = DateTime.Now, 
                    Is_Started = false, 
                    nw_Lat = 0 , 
                    nw_Lng = 20, 
                    se_Lat = 20, 
                    se_Lng = 20, 
                    image = null, 
                    GameConfigId = 2 
                }
                );

            //Player
            modelBuilder.Entity<Player>().HasData(
                new Player()
                {
                    Id = 1,
                    IsHuman = true,
                    IsPatientZero = true,
                    HungerTime = 10.0,
                    BiteCode = "1",
                    IsMuted = false,
                    GameId = 1,
                    UserId = "test",
                    FirstName = "adea",
                    LastName = "123"
                },
                new Player()
                {
                    Id = 2,
                    IsHuman = true,
                    IsPatientZero = false,
                    HungerTime = 10.0,
                    BiteCode = "2",
                    IsMuted = false,
                    GameId = 1,
                    UserId = "test",
                    FirstName = "yesbbb",
                    LastName = "aaaff"
                },
                new Player()
                {
                    Id = 3,
                    IsHuman = true,
                    IsPatientZero = true,
                    HungerTime = 10.0,
                    BiteCode = "3",
                    IsMuted = false,
                    GameId = 1,
                    UserId = "test",
                    FirstName = "arnold",
                    LastName = "tesi"
                }
                ); ;

            modelBuilder.Entity<Player>().HasMany(p => p.Chats).WithOne(p => p.Player);
            modelBuilder.Entity<Player>().HasMany(p => p.KillerStones).WithOne(g => g.Killer).HasForeignKey(g => g.KillerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Player>().HasOne(p => p.Victim).WithOne(g => g.Victim).HasForeignKey<Gravestone>(p => p.VictimId).OnDelete(DeleteBehavior.Restrict);



        }
    }
}
