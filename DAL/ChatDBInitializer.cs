using Common.Data;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace DAL
{
    /// <summary>
    /// The ChatDBInitializer class.
    /// Performs filling the content database.
    /// </summary>
    public class ChatDBInitializer : DropCreateDatabaseAlways<ChatDbContext>
    {

        protected override void Seed(ChatDbContext context)
        {
            IList<User> users = new List<User>
            {
                new User
                {
                    UserName = "dror",
                    Password = "123",
                    FirstName = "Dror",
                    LastName = "Rabinovich"
                },
                new User
                {
                    UserName = "yakov",
                    Password = "123",
                    FirstName = "Yakov",
                    LastName = "Abrramovich"
                },
                new User
                {
                    UserName = "afek",
                    Password = "123",
                    FirstName = "Afek",
                    LastName = "Keglefich"
                },
                new User
                {
                    UserName = "pazit",
                    Password = "123",
                    FirstName = "Pazit",
                    LastName = "Berdishev"
                },
                new User
                {
                    UserName = "niv",
                    Password = "123",
                    FirstName = "Niv",
                    LastName = "Pupkovich"
                },
                new User
                {
                    UserName = "david",
                    Password = "123",
                    FirstName = "David",
                    LastName = "Shlakbaum"
                },
                new User
                {
                    UserName = "misha",
                    Password = "123",
                    FirstName = "Michael",
                    LastName = "Krasavcheg"
                }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }


        }
    }
}