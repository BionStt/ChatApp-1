using Microsoft.EntityFrameworkCore;
using WouterFennis.ChatApp.Domain;

namespace WouterFennis.ChatApp.DAL
{
    public class ChatRoomContext : DbContext
    {
        public virtual DbSet<ChatRoom> ChatRooms { get; set; }

        public ChatRoomContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    OnderhoudsopdrachtMapping.Map(modelBuilder);
        //}
    }
}
