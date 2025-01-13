using AutoMapper;
using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Common.Dto;



namespace Service.Services
{
  
       
    //TODO להוסיף מה שצריך בשביל תמונות
public class MyMapper : Profile
    {
        public MyMapper()
        {
            // Map בין User ל-UserDto
            CreateMap<User, UserDto>().ReverseMap();

            // Map בין Post ל-PostDto
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.CreatedDate, src => 
                src.MapFrom(s => s.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")))
                .ReverseMap();

            // Map בין Comment ל-CommentDto
            CreateMap<Comment, CommentDto>().ReverseMap();

            // Map בין Challenge ל-ChallengeDto
            CreateMap<Challenge, ChallengeDto>().ReverseMap();

            // Map בין Achievement ל-AchievementDto
            CreateMap<Achievement, AchievementDto>().ReverseMap();

            // Map בין Follower ל-FollowerDto
            CreateMap<Follower, FollowerDto>().ReverseMap();

            // Map בין ChatMessage ל-ChatMessageDto
            CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
        }
    }

}

