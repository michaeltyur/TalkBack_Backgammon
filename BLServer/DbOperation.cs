using Common.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLServer
{
   public class DbOperation
    {
        ////private const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;
        ////                                        Initial Catalog=TalkBackDb;
        ////                                        Integrated Security=SSPI;";

        //////Find User in DB
        ////public static User GetUser(string username, string password)
        ////{
        ////    User user;

        ////    DataTable table = new DataTable();

        ////    using (SqlConnection conn = new SqlConnection(connectionString))
        ////    {
        ////        conn.Open();
        ////        SqlCommand command = new SqlCommand("select * from Users u where u.UserName=@username and u.Password=@password", conn);
        ////        command.Parameters.AddWithValue("@username", username);
        ////        command.Parameters.AddWithValue("@password", password);

        ////        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        ////        dataAdapter.Fill(table);
        ////        //conn.Close();
        ////    }
        ////    if (table.Rows.Count > 0)
        ////    {
        ////        var row = table.Rows[0];
        ////        user = new User
        ////        {
        ////            UserName = row[0].ToString(),
        ////            Password = row[1].ToString(),
        ////            FirstName = row[2].ToString(),
        ////            LastName = row[3].ToString()
        ////        };
        ////        return user;
        ////    }
        ////    else return null;
        ////}
        //////Add New User to DB
        ////public static User AddUser(User user)
        ////{
        ////    var _user = GetUser(user.UserName,user.Password);

        ////    if (_user != null) return null;

        ////    else _user = user;

        ////    using (SqlConnection connection = new SqlConnection(connectionString))
        ////    {
        ////        connection.Open();
        ////        // query here
        ////        String query = "INSERT INTO Users (username,password,firstname,lastname) VALUES (@username,@password,@firstname, @lastname)";

        ////        using (SqlCommand command = new SqlCommand(query, connection))
        ////        {
        ////            command.Parameters.AddWithValue("@username", _user.UserName);
        ////            command.Parameters.AddWithValue("@password", _user.Password);
        ////            command.Parameters.AddWithValue("@firstname", _user.FirstName);
        ////            command.Parameters.AddWithValue("@lastname", _user.LastName);


        ////            int result = command.ExecuteNonQuery();
                   
        ////            // Check Error
        ////            if (result < 0)
        ////            {
        ////                Console.WriteLine("Error inserting data into Database!");
        ////                return null;
        ////            }
        ////            else
        ////            {
        ////                //_user = GetUser(user.UserName, user.Password);
        ////                return _user;
        ////            }
        ////        }
        ////    }
        //}

        ////Get All Users from DB
        //public static List<User> GetAllUsers()
        //{
        //    List<User> listUsers = new List<User>(); ;

        //    DataTable table = new DataTable();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand("select * from Users", conn);

        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(table);
        //    }
        //    if (table.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in table.Rows)
        //        {
        //            User user = new User();

        //            listUsers.Add(
        //                new User
        //                {
        //                    UserName = row[0].ToString(),
        //                    Password = row[1].ToString(),
        //                    FirstName = row[2].ToString(),
        //                    LastName = row[3].ToString()
        //                });
        //        }
        //        return listUsers;
        //    }
        //    else return null;
        //}
    }
}
