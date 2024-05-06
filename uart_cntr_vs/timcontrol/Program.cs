using System;
using System.IO.Ports;
using System.Threading;

namespace timontrol
{
    class Program
    {
        static void Main(string[] args)
        {
            // Получаем список доступных COM-портов
            string[] ports = SerialPort.GetPortNames();
            SerialPort serialPort = new SerialPort();

            // Проверяем наличие COM-портов
            if (ports.Length == 0)
            {
                Console.WriteLine("COM-порты не найдены!");
                return;
            }

            // Выводим список доступных портов
            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {ports[i]}");
            }

            // Если доступен только один порт, выбираем его автоматически
            if (ports.Length == 1)
            {
                Console.WriteLine($"Выбран {ports[0]}");
                serialPort.PortName = ports[0];
            }
            // Иначе просим пользователя выбрать порт
            else
            {
                Console.Write("Введите номер COM-порта: ");
                int portNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                serialPort.PortName = ports[portNumber];
            }

            // Устанавливаем параметры для последовательного порта
            serialPort.BaudRate = 9600;
            serialPort.Open();

            // Инструкция для пользователя
            Console.WriteLine("Введите 'f' и частота (64, 128, 256)  или 'q' для выхода.");

            while (true)
            {
                // Читаем команду от пользователя
                string[] parts = Console.ReadLine().Split(' ');
                string cmd = parts[0].ToLower();

                // Выход из программы, если пользователь ввел 'q'
                if (cmd == "q")
                {
                    break;
                }

                // Обработка команды установки частоты ШИМ
                if (cmd == "f" && parts.Length > 1)
                {
                    byte frequencyValue = 0;
                    bool validFrequency = false;

                    // Определяем однобайтовую команду, соответствующую запрошенной частоте
                    switch (parts[1])
                    {
                        case "64":
                            frequencyValue = 1; // 64кГц
                            validFrequency = true;
                            break;
                        case "128":
                            frequencyValue = 2; // 128кГц
                            validFrequency = true;
                            break;
                        case "256":
                            frequencyValue = 3; // 256кГц
                            validFrequency = true;
                            break;
                    }

                    // Отправляем команду, если частота валидна
                    if (validFrequency)
                    {
                        serialPort.Write(new byte[] { frequencyValue }, 0, 1);
                        Console.WriteLine($"Команда установки частоты отправлена. Ожидаем эхо...");

                        // Ожидаем эхо-ответа в течение 2 секунд
                        var echoReceived = false;
                        var timeout = DateTime.Now.AddSeconds(2);
                        while (DateTime.Now < timeout)
                        {
                            if (serialPort.BytesToRead > 0)
                            {
                                byte echo = (byte)serialPort.ReadByte();
                                if (echo == frequencyValue)
                                {
                                    echoReceived = true;
                                    Console.WriteLine("Ok");
                                    break;
                                }
                            }
                            // Задержка для предотвращения чрезмерной нагрузки
                            Thread.Sleep(50);
                        }

                        if (!echoReceived)
                        {
                            Console.WriteLine("Error: Нет эхо-ответа или ответ некорректен.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверное значение частоты. Введите 64, 128 или 256.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверная команда.");
                }
            }

            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            Console.WriteLine("Программа завершена.");
        }
    }
}
