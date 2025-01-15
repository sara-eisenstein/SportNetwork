using Microsoft.EntityFrameworkCore;
using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock
{
    public class DataBase:DbContext,IContext
    {
        public DbSet<Achievement> achivevements { get ; set ; }
        public DbSet<Challenge> challenges { get ; set ; }
        public DbSet<ChallengeParticipant> challengeParticipants { get ; set ; }
        public DbSet<ChatMessage> chatMessages { get ; set ; }
        public DbSet<Comment> comments { get; set ; }
        public DbSet<Follower> followers { get ; set ; }
        public DbSet<Post> posts { get; set ; }
        public DbSet<User> users { get ; set ; }

        public void Save()
        {
            SaveChanges();
        }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-1VUANBN;database=sport_network;trusted_connection=true"); 
        }

    }
}
