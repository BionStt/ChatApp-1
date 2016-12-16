using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WouterFennis.ChatApp.Domain;

namespace WouterFennis.ChatApp.Managers
{
    public interface IChatRoomManager
    {
        IEnumerable<ChatRoom> GetAllChatRooms();
        ChatRoom FindChatRoomById(long id);
        void AddMessageToChatRoom(long chatRoomId, Message message);
        long AddChatRoom(ChatRoom chatRoom);
    }
}
