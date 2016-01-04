using Microsoft.ServiceBus.Messaging;
using System;
using System.Text;
using System.Threading;

namespace EventHubTestCapacity
{
    class Sender
    {
        string connectionString;
        string eventHubName;
        int eventCount;

        public Sender(string connectionString, string eventHubName, int eventCount)
        {
            this.connectionString = connectionString;
            this.eventCount = eventCount;
            this.eventHubName = eventHubName;
        }

        public void SendingRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
           for(int i=0;i< eventCount; i++)
                try
                {
                    var message = Guid.NewGuid().ToString();
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
                    eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

               Thread.Sleep(200);
            }
        }
    }
