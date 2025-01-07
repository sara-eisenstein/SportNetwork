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


            services.AddScoped<Irepository<User>, UserRepository>();
            services.AddScoped<Irepository<Post>, PostRepository>();
            services.AddScoped<Irepository<Follower>, FollowerRepository>();
            services.AddScoped<Irepository<Comment>, CommentRepository>();
            services.AddScoped<Irepository<ChatMessage>, ChatMessageRepository>();
            services.AddScoped<Irepository<ChallengeParticipant>, ChallengeParticipantRepository>();
            services.AddScoped<Irepository<Challenge>, ChallengeRepository>();
            services.AddScoped<Irepository<Achivevement>, AchievementRepository>();


            return services;
        }
    }
}
