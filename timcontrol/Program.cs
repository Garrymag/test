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
            string selectedPort;

            if (portNames.Length == 0)
            {
                Console.WriteLine("No serial ports found. Press Enter to exit.");
                Console.ReadLine();
                return;
            }

            if (portNames.Length == 1)
            {
                Console.WriteLine($"Only one serial port available: {portNames[0]}");
                selectedPort = portNames[0];
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
                selectedPort = portNames[portNumber - 1];

            }


            using (SerialPort port = new SerialPort(selectedPort))
            {
                Console.WriteLine($"Initializing port: {selectedPort}");
                port.BaudRate = 9600; // Установите скорость порта, соответствующую настройкам STM32
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                Console.WriteLine($"Speed: {port.BaudRate}");
                Console.WriteLine($"DataBits: {port.DataBits}");
                Console.WriteLine($"Parity: {port.Parity}");
                Console.WriteLine($"StopBits: {port.StopBits}");

                while (true)
                {
                    Console.WriteLine("Введите команду и параметры:");
                    string command = Console.ReadLine();

                    // Здесь можно обработать команду, чтобы проверить количество параметров
                    // и соответственно принять решение о дальнейших действиях
                    // Пример: Если команда "cmd1 123", распарсить и отправить
                    string[] parts = command.Split(' ');

                    switch (parts[0])
                    {
                        case "exit":
                            port.Close();
                            return;

                        case "freq":
                            { } break;
                        case "del1":
                            { } break;
                        case "dur1":
                            { } break;
                        case "del2":
                            { } break;
                        case "dur2":
                            { } break;
                        default:
                            { } break;

                    }

                    // Вам нужно будет реализовать логику проверки
                    // и форматирования команд здесь, в зависимости от протокола обмена с STM32

                    port.WriteLine(command); // Отправляем всю строку команды

                    // Для приема ответа (если необходимо), раскомментируйте следующие строки:
                    //string response = port.ReadLine();
                    //Console.WriteLine($"Ответ от STM32: {response}");
                }


            }
        }

    }
    
}

