using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorys.Entities;






namespace Repositorys.Interface
{
    public interface IContext
    {
        public DbSet<Achivevement> achivevements { get; set; }
        public DbSet<Challenge> challenges { get; set; }
        public DbSet<ChallengeParticipant> challengeParticipants { get; set; }
        public DbSet<ChatMessage> chatMessages{ get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Follower> followers { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<User> users { get; set; }

        void Save();

    }
}
