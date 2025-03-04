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
                .MapFrom(s => ConvertToByte(Environment.CurrentDirectory 
                + "/media/" + s.ProfilePicture)));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.ProfilePicture, src => src
                .MapFrom(s => s.File.FileName));



            // Map בין Post ל-PostDto
            CreateMap<Post, PostDto>()
            .ForMember(dest => dest.CreatedDate, src =>
        src.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")))
    .ForMember(dest => dest.Media, src =>
        src.MapFrom(src => ConvertToByte(Environment.CurrentDirectory+ "/media/"+ src.Media)))
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



        }
         public byte[] ConvertToByte(string img)
        {
            try
            {
                if (File.Exists(img))
                {
                    return System.IO.File.ReadAllBytes(img);
                }
                else
                {
                    // החזרת מערך ריק או תמונה ברירת מחדל במקרה שהקובץ לא קיים
                    Console.WriteLine($"קובץ לא נמצא: {img}");
                    return new byte[0]; // או החזרת תמונת ברירת מחדל
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה בעת המרת קובץ לבייטים: {ex.Message}");
                return new byte[0]; // או החזרת תמונת ברירת מחדל
            }
        }
    }

}

