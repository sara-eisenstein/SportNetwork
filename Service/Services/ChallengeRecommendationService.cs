using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repositorys.Entities;
using Repositorys.Interface;
using Service.Interfaces;
using System.Net.Http.Json;
using Common.Dto;
using System.Text.Json;

namespace Service.Services
{
    public class ChallengeRecommendationService : IChallengeRecommendationService
    {
        private readonly IRepository<Challenge> repository;
        private readonly IMapper mapper;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiEndpoint = "MY_API_KAY";

        public ChallengeRecommendationService(
            IRepository<Challenge> repository,
            IMapper mapper,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            _httpClient = httpClient;
            _apiKey = configuration["GeminiApi:ApiKey"];
        }

        public List<ChallengeDto> GetAll()
        {
            return mapper.Map<List<ChallengeDto>>(repository.GetAll());
        }

        public ChallengeDto Get(int id)
        {
            return mapper.Map<ChallengeDto>(repository.Get(id));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public ChallengeDto Update(ChallengeDto item, int id)
        {
            var entity = mapper.Map<Challenge>(item);
            return mapper.Map<ChallengeDto>(repository.Update(entity, id));
        }

        public ChallengeDto Add(ChallengeDto item)
        {
            var entity = mapper.Map<Challenge>(item);
            return mapper.Map<ChallengeDto>(repository.Add(entity));
        }

        public async Task<string> GetRecommendedChallenges(string userPrompt)
        {
           List<Challenge> availableChallenges = repository.GetAll();

            var request = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = $"{System.Text.Json.JsonSerializer.Serialize(availableChallenges)}\n\nהמשתמש שאל: {userPrompt}"
                            }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 1,
                    topK = 40,
                    topP = 0.95,
                    maxOutputTokens = 8192,
                    response_mime_type = "application/json",
                    response_schema = new
                    {
                        type = "ARRAY",
                        items = new
                        {
                            type = "OBJECT",
                            properties = new
                            {
                                challengeId = new { type = "INTEGER" }
                            }
                        }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}?key={_apiKey}", request);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<JsonDocument>();
            var candidates = result.RootElement.GetProperty("candidates")[0];
            var content = candidates.GetProperty("content");
            var parts = content.GetProperty("parts")[0];
            var text = parts.GetProperty("text").GetString();
  
            return text;
        }
    }
} 
