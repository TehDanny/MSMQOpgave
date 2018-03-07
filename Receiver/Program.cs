using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Receiver
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
            MessageQueue msMq2 = new MessageQueue(@".\private$\Q2csharp");
            msMq2.Formatter = new XmlMessageFormatter(new Type[] { typeof(Customer) });
            Console.WriteLine("***** RECEIVER *****");

            // Receive messages
            while (true)
            {
                var customer = (Customer)msMq2.Receive().Body;

                Console.WriteLine("Customer object recieved with the name: " + customer.Name + " and age: " + customer.Age + ".");
            }
        }
    }
}
