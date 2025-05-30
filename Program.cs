class Program
{
    static void Main()
    {
        Console.WriteLine("Старт...");

        dataArray = Enumerable.Range(1, 22).ToArray();
        Console.WriteLine($"Начальный поток: {string.Join(", ", dataArray)}");

        Task.Run(() => ModifyArray());
        Console.ReadKey();
    }

    static void ModifyArray()
    {
        Console.WriteLine("Модификация потока(первый поток)...");
        try 
        {
            mutex.WaitOne();
            for (int i = 0; i < dataArray.Length; i++)
            {
                Thread.Sleep(399);
                int randomNumber = random.Next(1, 11);
                dataArray[i] += randomNumber;
            }
            Console.WriteLine($"Модификация потока: {string.Join(", ", dataArray)}");

            Console.WriteLine("Модификация окончена.");
            Task.Run(() => FindMaxAndDisplay());
        }

        finally
        {
            mutex.ReleaseMutex();
        }

    }

    public static void FindMaxAndDisplay()
    {
        Console.WriteLine("Поиск максимального(второй поток)...");
        try
        {
            mutex.WaitOne();
            int max = dataArray.Max();
            for (int i = 0; i < dataArray.Length; i++)
            {
                Thread.Sleep(399);
            }
            Console.WriteLine($"Поиск максимального окончен: {max}");

        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    private static readonly Mutex mutex = new Mutex();
    private static int[] ?dataArray;
    private static readonly Random random = new Random();

}
