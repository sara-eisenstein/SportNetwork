using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


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

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public FitnessLevel Level { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string PasswordHash { get; set; }
        public string Goals { get; set; }
        public string Bio { get; set; }
        public bool Status { get; set; }
        public DateTime DateJoined { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public IFormFile? File { get; set; }
    }
}




