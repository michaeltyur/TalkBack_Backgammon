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

            Console.Title = "Chat Server";

            Uri adddress = new Uri("http://localhost/:8952/Chat");
            WSDualHttpBinding binding = new WSDualHttpBinding();
            Type contract = typeof(IChatService);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(30);
            binding.SendTimeout = TimeSpan.FromMinutes(30);

            var host = new ServiceHost(typeof(Service));

            host.AddServiceEndpoint(contract, binding, adddress);

            try
            {
                host.Open();

                Console.WriteLine("Running !!!");
                Console.WriteLine($"server address => {adddress}\nbinding => {binding.ToString()}\ncontract => {contract.ToString()}");
                Console.WriteLine($"baseAddress => http://localhost/:8952/ChatServer");

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
