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


    public class MyMapper : Profile
    {
        public MyMapper()
        {
            // Map בין User ל-UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ProfilePicture, src => src
                .MapFrom(s => ConvertToByte(Environment.CurrentDirectory + "/media/" + s.ProfilePicture)));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.ProfilePicture, src => src
                .MapFrom(s => s.File.FileName));



            // Map בין Post ל-PostDto
            CreateMap<Post, PostDto>()
    .ForMember(dest => dest.CreatedDate, src =>
        src.MapFrom(s => s.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")))
    .ForMember(dest => dest.Media, src =>
        src.MapFrom(s => ConvertToByte(Environment.CurrentDirectory + "/media/" + s.Media)))
    .ReverseMap();


            CreateMap<PostDto, Post>()
                .ForMember(dest => dest.Media, src => src
                .MapFrom(s => s.File.FileName));



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
            //map בין ChallengeParticipant ל ChallengeParticipantDto
            CreateMap<ChallengeParticipant, ChallengeParticipantDto>().ReverseMap();    

            // Map בין ChallengeParticipant ל-ChallengeParticipantDto

            CreateMap<ChallengeParticipant, ChallengeParticipantDto>().ReverseMap();



        }
        public byte[] ConvertToByte(string img)
        {
            var res = System.IO.File.ReadAllBytes(img);
            return res;
        }
    }

}

