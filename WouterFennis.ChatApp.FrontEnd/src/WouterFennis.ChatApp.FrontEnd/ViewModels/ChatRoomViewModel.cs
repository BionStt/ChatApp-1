using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WouterFennis.ChatApp.FrontEnd.Agents.Models;

namespace WouterFennis.ChatApp.FrontEnd.ViewModels
{
    public class ChatRoomViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Naam")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Berichten")]
        public List<MessageViewModel> Messages;

        public ChatRoomViewModel() { }

        public ChatRoomViewModel(ChatRoom chatRoom)
        {
            Id = chatRoom.Id;
            Name = chatRoom.Name;
            Messages = convertAgentMessagesToViewModel(chatRoom.Messages);
        }

        private List<MessageViewModel> convertAgentMessagesToViewModel(IEnumerable<Message> messages)
        {
            var query = from message in messages
                        select new MessageViewModel()
                        {
                            Id = message.Id,
                            Content = message.Content,
                            Sender = message.Sender,
                            CreatedOnUtc = message.CreatedOnUtc
                        };
            List<MessageViewModel> viewModelMessages = query.ToList();
            return viewModelMessages;
        }
    }
}
