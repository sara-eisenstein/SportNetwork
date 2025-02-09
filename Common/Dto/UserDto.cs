using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public enum FitnessLevel
    {
        Beginner,     // מתחיל - מתאים למשתמשים שרק התחילו להתאמן
        Intermediate, // בינוני - מתאים למשתמשים עם ניסיון מסוים באימונים
        Advanced,     // מתקדם - מתאים למשתמשים עם כושר גבוה
        Professional  // מקצוען - מתאים לספורטאים מקצועיים או מדריכי כושר
    }
    public class UserDto
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public FitnessLevel Level { get; set; }
        public string PasswordHash { get; set; }
        public string Goals { get; set; }
        public string Bio { get; set; }
        public bool Status { get; set; }
        public DateTime DateJoined { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public IFormFile? File { get; set; }
    }
}
