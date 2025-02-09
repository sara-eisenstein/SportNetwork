using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
=======
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> origin/correct_migrations

namespace Repositorys.Entities
{
    public class ChatMessage
    {
<<<<<<< HEAD
        [Key] public int MessageId { get; set; }
        

        public int SenderId { get; set; } // Foreign key to User
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

       
        public int RecipientId { get; set; } // Foreign key to User
=======
        [Key]
        public int MessageId { get; set; }

        [Required] // Sender ID cannot be null
        public int SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        [Required] // Recipient ID cannot be null
        public int RecipientId { get; set; }

>>>>>>> origin/correct_migrations
        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; }

        [Required]
        [MaxLength(1000)] // Optional, limit message size
        public string MessageContent { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
    }
}
