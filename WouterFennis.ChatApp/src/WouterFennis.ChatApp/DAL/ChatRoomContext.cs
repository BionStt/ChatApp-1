using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
