using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorys.Entities
{
    public class ChatMessage
    {
        [Key]
        public int MessageId { get; set; }

        [Required] // Sender ID cannot be null
        public int SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        [Required] // Recipient ID cannot be null
        public int RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; }

        [Required]
        [MaxLength(1000)] // Optional, limit message size
        public string MessageContent { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
    }
}
