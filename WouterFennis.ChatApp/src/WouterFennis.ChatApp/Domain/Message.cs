using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WouterFennis.ChatApp.Domain
{
    public class Message
    {
        [Required]
        public string Sender { get; set; }
        public DateTime CreatedOnUtc { get; private set; }
        [Required]
        public string Content { get; set; }

        public Message(string sender, string content)
        {
            Sender = sender;
            CreatedOnUtc = DateTime.UtcNow;
            Content = content;
        }
    }
}
