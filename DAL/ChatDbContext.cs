using Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// The ChatDBInitializer class.
    /// Performs mapping the content database to code;
    /// </summary>
    public class ChatDbContext: DbContext
    {
        public ChatDbContext():base("ChatDbContext")
        {
           // Database.SetInitializer(new ChatDBInitializer());
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
