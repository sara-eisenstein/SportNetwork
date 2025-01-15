using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public class ChatMessage
    {
        public int MessageId { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; } // Foreign key to User
        public virtual User Sender { get; set; }

        [ForeignKey("Recipient")]
        public int RecipientId { get; set; } // Foreign key to User
        public virtual User Recipient { get; set; }

        public string MessageContent { get; set; }
        public DateTime SentDate { get; set; }
    }
}
