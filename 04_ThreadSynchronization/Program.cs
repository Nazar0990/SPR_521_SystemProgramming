namespace _04_ThreadSynchronization
{
    class Counter
    {
        public static int count = 0;
    }
    class LockCounter
    {
        private int number;//4   1  2 3 4
        public int Number { get { return number; } }
        private int evenNumbers;//4
        public int EvenNumbers { get { return evenNumbers; } }

        public void UpdateFiels()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                lock (this)//
                {
                    number++;
                    if (number % 2 == 0)
                        evenNumbers++;
                }

            }
        }
    }
    static class LockCounterStatic
    {
        static int number;//2
        static int evenNumber;//2
        public static int Number { get { return number; } }
        public static int EvenNumbers { get { return evenNumber; } }
        public static void UpdateFiels()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                lock (typeof(LockCounterStatic))
                {
                    number++;// 2
                    if (number % 2 == 0)
                        evenNumber++;

                }

            }
        }
    }
    #region Sync with Monitor
    class MonitorLockCounter
    {
        int number;
        int even;
        public int Number { get { return number; } }
        public int Even { get { return even; } }
        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                //Monitor.Enter(this);

                //string str = null; str.ToString(); // NullReferenceException

                //++number;
                //if (number % 2 == 0)
                //    ++even;

                //Monitor.Exit(this);

                Monitor.Enter(this); // block this class fileds in other threads
                try
                {
                    ++number;
                    //ex
                    if (number % 2 == 0)
                        //ex
                        ++even;
                    //ex

                }
                finally
                {
                    Monitor.Exit(this); // unblock
                }
            }
        }

        private static void GoodAsync()
        {
            Console.WriteLine("Sync Monitor-methods:");
            MonitorLockCounter c = new MonitorLockCounter();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Number, c.Even);
        }
    }
    #endregion

    class Statistics
    {
        public static int Letters { get; set; }
        public static int Digits { get; set; }
        public static int Words { get; set; }
    }
    internal class Program
    {

        static void TextAnalize(string text, Statistics statistics)
        {
            //algorihtm
        }
        static void Main(string[] args)
        {
            #region Homework
            Statistics statistics = new Statistics();
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Test");
            foreach (var item in files)
            {
                Console.WriteLine(item);

                string text = File.ReadAllText(item);
                Console.WriteLine(text);
                Thread thread = new Thread(() => TextAnalize(text, statistics));
            }

            #endregion

            #region Thread problems

            /*
             Thread[] threads = new Thread[5];
             for (int i = 0; i < threads.Length; i++)
             {
                 threads[i] = new Thread(delegate ()
                 {
                     for (int i = 0; i < 1_000_000; i++)
                     {
                         Counter.count++;
                     }
                 });
                 threads[i].Start();
             }

             for (int i = 0; i < threads.Length; i++)
             {
                 threads[i].Join();
             }

             Console.WriteLine($"Counter : {Counter.count}");
            */
            #endregion
            #region Thread Sync
            ////Interlocked.Increment - add + 1
            ////Interlocked.Decrement - add - 1
            ////Interlocked.Add - add + value
            ////Interlocked.Exchange - add - value
            /*
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(delegate ()
                {
                    for (int i = 0; i < 1_000_000; i++)
                    {
                        Interlocked.Increment(ref Counter.count);//Counter.count++;
                    }
                });
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine($"Counter : {Counter.count}");
            */
            #endregion

            #region Thread Hard data
            LockCounter lockCounter = new LockCounter();
            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(lockCounter.UpdateFiels);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            //5_000_000   ---> 2_500_000
            Console.WriteLine($"All numbers : {lockCounter.Number}\t" +
                $"Even numbers : {lockCounter.EvenNumbers}");

            #endregion


        }
    }

}
