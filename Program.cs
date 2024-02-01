using System;
using System.Threading;

class BarberShop
{
    private readonly object barberChairLock = new object();
    private int availableChairs = 2; // Кількість вільних крісел у приймальні


    public void CustomerArrival(int customerId)
    {
        if (availableChairs > 0) // Спробуємо зайняти вільне крісло у приймальні
        {
            availableChairs--;
            Console.WriteLine($"Customer {customerId} came and waiting");
            lock (barberChairLock)
            {
                availableChairs++;
                Console.WriteLine($"Customer {customerId} woke up the barber");
                Console.WriteLine("Haircut is going...");
                Thread.Sleep(5000);
                Console.WriteLine("Haircut ended, barber sleeps");
                Console.WriteLine($"Customer {customerId} left after haircut");
            }
        }
        else
        {
            Console.WriteLine($"Customer {customerId} came and left, no waiting chairs available");
        }
    }

}

class Program
{
    static void Main()
    {
        BarberShop barberShop = new BarberShop();
        int customerId = 0;
        Random random = new Random();

        while (true)
        {
            Thread customerThread = new Thread(() => barberShop.CustomerArrival(customerId++));
            customerThread.Start();
            Thread.Sleep(random.Next(1000, 2000)); // Випадкова затримка між приходами відвідувачів
        }
    }
}
