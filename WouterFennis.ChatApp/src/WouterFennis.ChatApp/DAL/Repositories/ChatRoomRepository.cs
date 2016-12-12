using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WouterFennis.ChatApp.Domain;

namespace WouterFennis.ChatApp.DAL.Repositories
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
    }
}
