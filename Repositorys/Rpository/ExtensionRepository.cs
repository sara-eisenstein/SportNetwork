using Microsoft.Extensions.DependencyInjection;
using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public static class ExtensionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {


            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Post>, PostRepository>();
            services.AddScoped<IRepository<Follower>, FollowerRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<ChatMessage>, ChatMessageRepository>();
            services.AddScoped<IRepository<ChallengeParticipant>, ChallengeParticipantRepository>();
            services.AddScoped<IRepository<Challenge>, ChallengeRepository>();
            services.AddScoped<IRepository<Achievement>, AchievementRepository>();


            return services;
        }
    }
}
