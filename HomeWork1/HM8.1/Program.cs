using System;
using System.Text;
using System.Threading;

class BarberShop
{
    static Semaphore barberChair = new Semaphore(1, 1); 
    static Semaphore waitingRoom = new Semaphore(5, 5);
    static AutoResetEvent barberSleep = new AutoResetEvent(false); 
    static object lockObj = new object(); 

    static int waitingClients = 0; 
    static bool barberSleeping = true; 
    public static void Barber()
    {
        while (true)
        {
            lock (lockObj)
            {
                if (waitingClients == 0)
                {
                    barberSleeping = true;
                    Console.WriteLine(" Закінчилися клієнти перукар засинає...");
                }
            }

            barberSleep.WaitOne(); 

            while (true)
            {
                lock (lockObj)
                {
                    if (waitingClients == 0)
                    {
                        break;
                    }
                    
                    waitingClients--;
                }

                barberChair.WaitOne();
                Console.WriteLine("Перукар стриже клієнта...");
                Thread.Sleep(3000); 
                Console.WriteLine("Клієнт готовий, перукар звільнив крісло!");
                barberChair.Release();
            }
        }
    }

    public static void Client(object? id)
    {
        int clientId = (int)id;

        if (!waitingRoom.WaitOne(0)) 
        {
            Console.WriteLine($"Клієнт {clientId} не знайшов місця і пішов.");
            return;
        }

        lock (lockObj)
        {
            waitingClients++;
            Console.WriteLine($"Клієнт {clientId} сів у приймальні.");

            if (barberSleeping)
            {
                barberSleeping = false;
                Console.WriteLine($"Клієнт {clientId} розбудив перукаря!");
                barberSleep.Set();
            }
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        

        
        Thread barberThread = new Thread(Barber);
        barberThread.Start();

        for (int i = 1; i <= 10; i++)
        {
            Thread.Sleep(new Random().Next(500, 1500)); 
            Thread clientThread = new Thread(Client);
            clientThread.Start(i);
            
            
        }
    }
}


//using System.Threading;
//using System.Text;
//using System;
//Console.OutputEncoding = Encoding.UTF8;

//for (int i = 1; i < 8; i++)
//{
//    new Client(i);
//}
//public class Client
//{
//    static Semaphore barbersChair = new Semaphore(1, 1);
//    static Semaphore queue = new Semaphore(5, 5);
//    Thread myThread;
//    public Client(int i)
//    {
//        myThread = new Thread(Procces);
//        myThread.Name = $"Клієнт {i}";
//        myThread.Start();
//    }

//    public void Procces()
//    {
//        {
//            if (!queue.WaitOne(0)) // Перевіряємо, чи є місце в черзі
//            {
//                Console.WriteLine($"{Thread.CurrentThread.Name} не знайшов місця в прихожій і пішов.");
//                return;
//            }

//            Console.WriteLine($"{Thread.CurrentThread.Name} зайняв місце в черзі.");

//            barbersChair.WaitOne(); // Очікування на крісло барбера

//            Console.WriteLine($"{Thread.CurrentThread.Name} сідає до крісла барбера.");
//            Console.WriteLine($"{Thread.CurrentThread.Name} Відбувається процес стрижки...");
//            Thread.Sleep(2000);

//            Console.WriteLine($"{Thread.CurrentThread.Name} встає та звільняє крісло.");
//            barbersChair.Release(); // Звільняємо крісло

//            queue.Release(); // Звільняємо місце в черзі
//        }
//    }
//}
