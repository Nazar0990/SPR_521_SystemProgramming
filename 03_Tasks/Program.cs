namespace _03_Tasks
{
    internal class Program
    {
        static void Method()
        {
            Console.WriteLine($"Id of primary thread : {Thread.CurrentThread.GetHashCode()}");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"\t\t\t { i } - Hello in thread");
            }
        }
        static void ThreadFunct(object a)
        {
            string ID = (string)a;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(ID);
                //Thread.Sleep(100);
            }
        }
        static void Main(string[] args)
        {
            
            //Example 1
           // ThreadStart// void()
            //ParameterizedThreadStart // void (object a);
            //Thread thread = new Thread(Method);
            //thread.Start();
            ////Method();
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine($"\t {i} - Hello in main");
            //}


            //Example 2
            //Thread thread1 = new Thread(ThreadFunct);
            //thread1.Priority = ThreadPriority.Highest;
            //thread1.Start("One");

            //Thread thread2 = new Thread(ThreadFunct);
            //thread2.Start("\tTwo");

            //Console.WriteLine("End!!!!");

            //Example 3
            //Thread thread = new Thread(Method);
            //thread.IsBackground = true;
            //thread.Start();

            //Console.WriteLine($"Id of primary thread : {Thread.CurrentThread.GetHashCode()}");

            //Console.ReadKey();
            //Console.WriteLine("Main end!");

            //Example 4
            //ThreadStart ts = new ThreadStart(Method);
            //Thread t = new Thread(ts);
            //t.Start(); // Запуск потока.
            //Console.WriteLine("Press any key to pause thread...");

            //Console.ReadKey();
            //t.Suspend(); // Приостановка потока.
            //Console.WriteLine("Thread is stoped!");
            //Console.WriteLine("Press any key to resume thread...");

            //Console.ReadKey();
            //t.Resume(); // Возобновление работы.

            /* Thread Priorities:
            ■ Highest 
            ■ AboveNormal 
            ■ Normal (default)
            ■ BelowNormal 
            ■ Lowest
          *///ThreadStart t = new ThreadStart(Method);
            //ParameterizedThreadStart ts = new ParameterizedThreadStart(Method1);

            //Thread t1 = new Thread(ts);
            //Thread t2 = new Thread(ts);

            //t1.Priority = ThreadPriority.Lowest;
            //t2.Priority = ThreadPriority.Highest;

            //Console.ReadKey();
            //t1.Start((object)"t1: Lowest");
            //t2.Start((object)"\t\t\tt2: Highest");
            //Console.WriteLine("Hello top");
            //Console.ReadKey();
            //Console.WriteLine("Hello bottom");


            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                //Thread thread = new Thread(InfinityLoop);   
                //thread.Start();
                InfinityLoop();

                Console.WriteLine("Some message???????????");
            } while (input.Key != ConsoleKey.Escape);

            Tuple<int, int> tuple = new Tuple<int, int>(1, 25);
            
        }
        static void InfinityLoop()
        {
            Console.WriteLine("Thred has started...");
            while (true)
            {
                int a = 5;
                int b = 10;
                int c = a + b;
                new Random().Next();
            }
        }
        static void Method1(object str)
        {
            string text = (string)str;
            for (int i = 0; i < 200; i++)
            {
                Console.WriteLine("{0} #{1}", text, i.ToString());
            }
        }
    }
}
