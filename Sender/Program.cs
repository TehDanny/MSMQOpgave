using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }

        private void Run()
        {
            // Initialize
            string queueName1 = @".\private$\Q1xml";
            string queueName2 = @".\private$\Q2csharp";
            MessageQueue msMq1 = null;
            MessageQueue msMq2 = null;

            if (!MessageQueue.Exists(queueName1))
            {
                msMq1 = MessageQueue.Create(queueName1);
            }
            else
            {
                msMq1 = new MessageQueue(queueName1);
            }

            if (!MessageQueue.Exists(queueName2))
            {
                msMq2 = MessageQueue.Create(queueName2);
            }
            else
            {
                msMq2 = new MessageQueue(queueName2);
            }

            Console.WriteLine("***** SENDER *****");

            // Request userinput
            Console.WriteLine("What is your name?");
            string customerName = Console.ReadLine();
            Console.WriteLine("How old are you?");
            int customerAge = Int32.Parse(Console.ReadLine());

            // Send message to translator
            string message = @"<Customer><Name>" + customerName + @"</Name><Age>" + customerAge + @"</Age></Customer>";

            msMq1.Send(message);

            Console.ReadLine();
        }
    }
}
