using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;





namespace timcontrol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] portNames = SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                Console.WriteLine("No serial ports found. Press Enter to exit.");
                Console.ReadLine();
                return;
            }

            if (portNames.Length == 1)
            {
                Console.WriteLine($"Only one serial port available: {portNames[0]}");
                InitializePort(portNames[0]);
            }
            else
            {
                Console.WriteLine("Available serial ports:");
                for (int i = 0; i < portNames.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {portNames[i]}");
                }

                Console.WriteLine("Enter the port number you want to use:");

                int portNumber;
                while (!int.TryParse(Console.ReadLine(), out portNumber) || portNumber <= 0 || portNumber > portNames.Length)
                {
                    Console.WriteLine("Invalid port number, try again:");
                }

                // Array is zero-based, user input is 1-based.
                string selectedPort = portNames[portNumber - 1];
                InitializePort(selectedPort);
            }
        }

        static void InitializePort(string portName)
        {
            // Here you would initialize your serial port and start communication
            // As an example, just printing the selected port
            Console.WriteLine($"Initializing port: {portName}");
            // More serial port initialization code would go here...

            // Keep the console window open
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
    
}

