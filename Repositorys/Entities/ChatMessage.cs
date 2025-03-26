using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace Repositorys.Entities
{
    public class ChatMessage
    {

        [Key] public int MessageId { get; set; }

        [Required] // Sender ID cannot be null
        public int SenderId { get; set; } // Foreign key to User

        [JsonIgnore]

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        [Required] // Recipient ID cannot be null
        public int RecipientId { get; set; } // Foreign key to User
        [JsonIgnore]

        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; }

        [Required]
        [MaxLength(1000)] // Optional, limit message size
        public string MessageContent { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
    }
}
