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



    static void FindMaxAndDisplay()
    {
        lock (lockObject)
        {
            int max = dataArray.Max();
            Console.WriteLine("Поиск максимального(второй поток)...");
            for (int i = 0; i < dataArray.Length; i++)
            {
                Thread.Sleep(399);
            }
            Console.WriteLine($"Поиск максимального окончен: {max}");

        }
    }

    private static readonly object lockObject = new object();
    private static int[] dataArray;
    private static readonly Random random = new Random();

}
