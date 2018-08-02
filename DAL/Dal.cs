using Common.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// The main Math class.
    /// Contains methods for performing basic operations with DB.
    /// </summary>
    /// /// <remarks>
    /// <para>This class can add, delete, update objects of DB.</para>
    /// </remarks>
    public class Dal
    {
        #region Users
        //Find User in DB
        public User GetUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("One or more parameters are null");
            }
            User user;

            using (ChatDbContext context = new ChatDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            }
            return user;
        }
        //Find User in DB
        public User GetUserByName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("One or more parameters are null");
            }
            User user;

            using (ChatDbContext context = new ChatDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.UserName == username);
            }
            return user;
        }
        //Add New User to DB
        public void AddUser(User user)
        {
            if (user != null)
            {
                using (ChatDbContext context = new ChatDbContext())
                {
                    try
                    {
                        user.UserId = context.Users.Count();
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("En Error during registration", ex);
                    }
                }
            }
            else throw new Exception("The user object is null");
        }
        //Add New User to DB
        public void UpdateUser(User user)
        {
            if (user != null)
            {
                using (ChatDbContext context = new ChatDbContext())
                {
                    var oldUser = GetUserByName(user.UserName);
                    try
                    {
                        oldUser.Password = user.Password;
                        oldUser.FirstName = user.FirstName;
                        oldUser.LastName = user.LastName;
                        //oldUser.Messages = user.Messages;

                        context.Users.Add(user);

                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception( ex.Message);
                    }
                }
            }
            else throw new Exception("The user object is null");
        }
        //Get All Users from DB
        public List<User> GetAllUsers()
        {
            List<User> listUsers = new List<User>(); ;

            using (ChatDbContext context = new ChatDbContext())
            {
                try
                {
                    listUsers = context.Users.ToList();
                }
                catch (Exception ex)
                {

                    throw new Exception("En Error during getting all users", ex.InnerException);
                }
            }
            return listUsers;
        }
        //Delete User
        public void DeleteUser(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                User user = GetUser(userName, password);
                if (user != null)
                {
                    using (ChatDbContext context = new ChatDbContext())
                    {
                        try
                        {
                            context.Users.Remove(user);
                            context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                            throw new Exception("En Error during removing the user", ex.InnerException);
                        }
                    }
                }
            }
        }

        #endregion

        #region Message
        //Add New Message to DB
        public void AddMessage(Message msg)
        {
            if (msg != null)
            {
                using (ChatDbContext context = new ChatDbContext())
                {
                    var user = GetUserByName(msg.UserName);

                    try
                    {
                        var count = context.Messages.Count();
                        if (count > 1000)
                        {
                            DeleteAllMessage();
                        }
                        msg.MessageId = context.Messages.Count();
                        context.Messages.Add(msg);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

            }
            else throw new Exception("The message object is null");
        }

        //Get All Message from selected user
        public ICollection<Message> GetMessagePerUser(User user)
        {
            if (user != null)
            {
                List<Message> messages = new List<Message>();
                using (ChatDbContext context = new ChatDbContext())
                {
                    try
                    {
                        messages = context.Messages.Where(m => m.UserName == user.UserName).ToList();
                        return messages;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("En Error during registration", ex);
                    }
                }
            }
            else throw new Exception("The user object is null");
        }

        //Remove All Message
        public void DeleteAllMessage()
        {
            using (ChatDbContext context = new ChatDbContext())
            {
                try
                {
                    context.Messages.RemoveRange(context.Messages);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("En Error during registration", ex);
                }
            }
        }

        #endregion
    }



}
