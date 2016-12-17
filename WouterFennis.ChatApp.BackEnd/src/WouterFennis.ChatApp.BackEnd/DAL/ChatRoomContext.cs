using Microsoft.EntityFrameworkCore;
using WouterFennis.ChatApp.BackEnd.Domain;

namespace WouterFennis.ChatApp.BackEnd.DAL
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
