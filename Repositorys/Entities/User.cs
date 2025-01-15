using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public enum FitnessLevel
    {
        Beginner,     // מתחיל - מתאים למשתמשים שרק התחילו להתאמן
        Intermediate, // בינוני - מתאים למשתמשים עם ניסיון מסוים באימונים
        Advanced,     // מתקדם - מתאים למשתמשים עם כושר גבוה
        Professional  // מקצוען - מתאים לספורטאים מקצועיים או מדריכי כושר
    }
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FitnessLevel Level { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePicture { get; set; }
        public string Goals { get; set; }
        public string Bio { get; set; }
        public bool Status { get; set; }
        public DateTime DateJoined { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Follower> Followers{ get; set;}
    }
}
