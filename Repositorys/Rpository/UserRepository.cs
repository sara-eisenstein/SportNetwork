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
            entity.DateJoined = DateTime.UtcNow;
            entity.Status = true;
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

        public User Update(User entity,int id)
        {
            // מציאת הישות הקיימת ב-DB על פי ID
            User user = Get(id);

            if (user == null)
            {
                throw new Exception($"User with ID {entity.UserId} not found.");
            }

            // עדכון השדות הרצויים

            user.Email = entity.Email;
            if (entity.ProfilePicture != null)
            {
                user.ProfilePicture = entity.ProfilePicture;


            }
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.DateJoined = entity.DateJoined;
            user.Bio = entity.Bio;
            user.Status = entity.Status;
            user.Level = entity.Level;
            user.Goals = entity.Goals;

            // שמירת השינויים ל-DB

            _context.users.Update(user);
            _context.Save();

            return user;
        }

        public void Save()
        {
            
        }
    }
}
