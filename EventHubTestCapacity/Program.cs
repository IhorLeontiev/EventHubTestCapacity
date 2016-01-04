using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using System.Diagnostics;

namespace EventHubTestCapacity
{
    class Program
    {
        static string eventHubName = "";
        static string connectionString = "";
        static void Main(string[] args)
        {


            System.Net.ServicePointManager.DefaultConnectionLimit = 1024;
            Console.WriteLine("Start sending ...");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Paralelize();
            sw.Stop();
            Console.WriteLine("Completed in {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("Press enter key to stop worker.");
            Console.ReadLine();

        }


        static void Paralelize()
        {
            Task[] tasks = new Task[25];
            for (int i = 0; i < 25; i++)
            {
                tasks[i] = new Task(() => Send(500));
            }

            Parallel.ForEach(tasks, (t) => { t.Start(); });
            Task.WaitAll(tasks);
        }

        public static void Send(int eventCount)
        {
            Sender s = new Sender(connectionString, eventHubName, eventCount);
            s.SendingRandomMessages();
        }


   
    }
}
