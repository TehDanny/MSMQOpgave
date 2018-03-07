using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.IO;
using System.Xml.Serialization;

namespace Translator
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
            MessageQueue msMq1 = new MessageQueue(@".\private$\Q1xml");
            MessageQueue msMq2 = new MessageQueue(@".\private$\Q2csharp");
            msMq1.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            Console.WriteLine("***** TRANSLATOR *****");

            while (true)
            {
                // Receive message from sender
                var message = (string)msMq1.Receive().Body;

                using (TextReader stringReader = new StringReader(message))
                {
                    // Translate message
                    var serializer = new XmlSerializer(typeof(Customer));
                    Customer customer = (Customer)serializer.Deserialize(stringReader);

                    // send
                    Console.WriteLine("Translated a XML document to a Customer object with the name: " + customer.Name + " and age: " + customer.Age + ".");
                    Console.WriteLine("Forwarded Customer object to receiver");
                    msMq2.Send(customer);
                }
            }
        }
    }
}
