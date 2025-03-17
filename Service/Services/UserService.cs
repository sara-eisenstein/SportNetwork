using AutoMapper;
using Repositorys.Entities;
using Repositorys.Interface;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;
using Microsoft.AspNetCore.Identity;


namespace Service.Services
{
    public class UserService : IService<UserDto>
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;
        private readonly PasswordHasher<string> _passwordHasher;


        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._passwordHasher = new PasswordHasher<string>();
        }

        public UserDto Add(UserDto item)
        {
            var entity = mapper.Map<User>(item);

            // הצפנת הסיסמה לפני השמירה
            entity.PasswordHash = _passwordHasher.HashPassword(null, item.PasswordHash);

            // שמירת המשתמש בבסיס הנתונים
            var savedEntity = repository.Add(entity);

            return mapper.Map<UserDto>(savedEntity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public UserDto Get(int id)
        {
            return mapper.Map<UserDto>(repository.Get(id));
        }

        public List<UserDto> GetAll()
        {
            return mapper.Map<List<UserDto>>(repository.GetAll());
        }

        public UserDto Update(UserDto item, int id)
        {
            var entity = mapper.Map<User>(item);

            // בדיקה אם המשתמש סיפק סיסמה חדשה, ואם כן – להצפין אותה
            if (!string.IsNullOrEmpty(item.PasswordHash))
            {
                entity.PasswordHash = _passwordHasher.HashPassword(null, item.PasswordHash);
            }
            else
            {
                // אם הסיסמה ריקה, נשמור את הסיסמה הנוכחית של המשתמש הקיים
                var existingUser = repository.Get(id);
                entity.PasswordHash = existingUser?.PasswordHash;
            }

            return mapper.Map<UserDto>(repository.Update(entity, id));
        }

    }
}
