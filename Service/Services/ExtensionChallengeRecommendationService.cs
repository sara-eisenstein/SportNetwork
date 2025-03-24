using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;

namespace Service.Services
{
    public static class ExtensionChallengeRecommendationService
    {
        public static void AddChallengeRecommendationService(this IServiceCollection services)
        {
            services.AddScoped<IChallengeRecommendationService, ChallengeRecommendationService>();
        }
    }
} 