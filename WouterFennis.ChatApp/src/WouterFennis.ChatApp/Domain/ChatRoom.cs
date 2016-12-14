using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WouterFennis.ChatApp.Domain
{
    public class ChatRoom
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Message> Messages { get; set; }


        public ChatRoom()
        {

        }


        public ChatRoom(string name)
        {
            Name = name;
            Messages = new List<Message>();
        }
    }
}
