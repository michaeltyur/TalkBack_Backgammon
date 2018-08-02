using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// The main Math class.
    /// Contains all methods for performing operations with DB.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CreateDb();
            Console.WriteLine();
            CheckUser();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        private static void CreateDb()
        {
            Dal dal = new Dal();

            List<User> list = dal.GetAllUsers();

            foreach (var user in list)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} will be created");
            }
        }
        private static void CheckUser()
        {
            Dal dal = new Dal();

            Console.WriteLine("Please type username");
            var userName= Console.ReadLine();
            Console.WriteLine("Please type password");
            var password = Console.ReadLine();
            if (!string.IsNullOrEmpty(userName)&& !string.IsNullOrEmpty(password))
            {
                var user = dal.GetUser(userName, password);
                Console.WriteLine($"{user.FirstName} {user.LastName} exsist in DB");
            }
        }
        private static void DeleteAllUsers()
        {
            Dal dal = new Dal();

            List<User> list = dal.GetAllUsers();

            foreach (var user in list)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} will be deleted");
                dal.DeleteUser(user.UserName, user.Password);
            }

        }

    }
}
