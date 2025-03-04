using Microsoft.Extensions.DependencyInjection;
using Repositorys.Entities;
using Repositorys.Interface;
using Repositorys.Rpository;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;




namespace Service.Services
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {

            services.AddRepository();
            services.AddScoped<IService<UserDto>, UserService>();
            services.AddScoped<IService<PostDto>, PostService>();
            services.AddScoped<IService<FollowerDto>, FollowerService>();
            services.AddScoped<IService<CommentDto>, CommentService>();
            services.AddScoped<IcommentService, ExtensionCommentService>();
            services.AddScoped<IService<ChatMessageDto>, ChatMessageService>();
            services.AddScoped<IChatMessageService, ExtensionChatMessageService>();
            services.AddScoped<IService<ChallengeParticipantDto>, ChallengeParticipantService>();
            services.AddScoped<IService<ChallengeDto>, ChallengeService>();
            services.AddScoped<IChallengeService, ExtensionChallengeService>();
            services.AddScoped<IService<AchievementDto>, AchievementService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddAutoMapper(typeof(MyMapper));
            return services;
        }
    }
}
