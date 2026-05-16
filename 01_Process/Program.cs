using System.Diagnostics;

namespace _01_Process
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Process process = new Process();
            //Process current = Process.GetCurrentProcess();
            //current.PriorityClass = ProcessPriorityClass.High;    
            //Console.WriteLine("------------- Current process info---------");
            //Console.WriteLine($"PriorityClass : {current.PriorityClass}");
            //Console.WriteLine($"ProcessName : {current.ProcessName}");
            //Console.WriteLine($"Id : {current.Id}");
            //Console.WriteLine($"MachineName : {current.MachineName}");
            //Console.WriteLine($"PrivateMemorySize64 : {current.PrivateMemorySize64/1024}");
            //Console.WriteLine($"StartTime : {current.StartTime}");
            //Console.WriteLine($"TotalProcessorTime : {current.TotalProcessorTime}");
            //Console.WriteLine($"UserProcessorTime : {current.UserProcessorTime}");

            #region Get All Processes
            //Process [] processes = Process.GetProcesses();
            //Console.WriteLine("Process Name \tPID\tPriority\tMachine name\tStart time");
            //foreach (Process p in processes)
            //{
            //    try
            //    {
            //        Console.WriteLine($"{p.ProcessName}\t{p.Id}\t{p.BasePriority}" +
            //       $"\t{p.MachineName}\t{p.StartTime}");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.Write(p.ProcessName + " ");
            //        Console.WriteLine(ex.Message);
            //        Console.ResetColor();   
            //    }

            //}
            #endregion

            #region Start process
            //Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", 
            //    "google.com mystat.itstep.org stackoverflow.com");
            //Process.Start("calc.exe");

            ProcessStartInfo info = new ProcessStartInfo()
            { 
                FileName = @"notepad",
                Arguments = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\777.txt",
                WindowStyle = ProcessWindowStyle.Maximized
            };

            Process pr = Process.Start(info);
            Console.WriteLine("Press any key");
            Console.ReadKey();
            pr.Close();//clear reference
            //pr.CloseMainWindow();
            //pr.Kill();

            pr.WaitForExit();
            Console.WriteLine($"ExitCode {pr.ExitCode}");
            Console.WriteLine($"ExitTime {pr.ExitTime}");
          
            #endregion

            Console.ReadKey();  
        }
    }
}
