using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WouterFennis.ChatApp.BackEnd.Domain;

namespace WouterFennis.ChatApp.BackEnd.DAL.Repositories
{
    public class ChatRoomRepository : BaseRepository<ChatRoom, long, ChatRoomContext>
    {
        public ChatRoomRepository(ChatRoomContext context) : base(context)
        {
        }

        protected override DbSet<ChatRoom> GetDbSet()
        {
            return _context.ChatRooms;
        }

        protected override long GetKeyFrom(ChatRoom item)
        {
            return item.Id;
        }

        public override IEnumerable<ChatRoom> FindByIdWithMessages(long id)
        {
            return GetDbSet().Where(chatRoom => chatRoom.Id == id).Include(chatRoom => chatRoom.Messages).ToList();
        }
    }
}
