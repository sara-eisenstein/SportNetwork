using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class UserRepository : IRepository<User>
    {
        private readonly IContext _context;
        public UserRepository(IContext context)
        {
            _context = context;
        }
        public User Add(User entity)
        {
            _context.users.Add(entity);
            _context.Save();
            return entity;
        }

        public void Delete(int id)
        {
            _context.users.Remove(Get(id));
            _context.Save();
              
        }

        public User Get(int id)
        {
            return _context.users.FirstOrDefault(x=>x.UserId==id);
        }

        public List<User> GetAll()
        {
            return _context.users.ToList();  
        }

        public User Update(User entity)
        {
            User user = Get(entity.UserId);
            user.ProfilePicture = entity.ProfilePicture;
            user.Email = entity.Email;
            user.FirstName = entity.FirstName;  
            user.LastName = entity.LastName;    
            user.DateJoined = entity.DateJoined;
            user.Bio=entity.Bio; 
            user.Status = entity.Status;
            user.Level = entity.Level;  
            _context.users.Add(user);
            _context.Save();
            return user;


        }
        public void Save()
        {
            
        }
    }
}
