using Common.Interface;
using Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    /// <summary>
    /// Сreates and configures host service 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(60, 7);

            Console.Title = "TalkBack_BackGamon Server";

            var host = new ServiceHost(typeof(Service));

            try
            {
                host.Open();

                Console.WriteLine("TalkBack Server is running !!!");
                Console.WriteLine($"baseAddress for app=> http://localhost/:8952/BackTalkServer");

                Console.ReadLine();

                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
