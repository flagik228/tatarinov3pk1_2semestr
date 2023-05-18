using System;
using System.Diagnostics;

namespace pz1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 1000000;
            int[] a = new int[n];
            Random rmd = new Random();
            for (int i = 0; i < a.Length; i++)
            {

                a[i] = rmd.Next(0, 100000);
            }
            Timing objT = new Timing();
            Stopwatch stpWatch = new Stopwatch();
            objT.StartTime();
            stpWatch.Start();
            SimpleSearch(a, n);
            stpWatch.Stop();
            objT.StopTime();
            Console.WriteLine("Простой поиск StopWatch: " + stpWatch.Elapsed.ToString());
            Console.WriteLine("Бинарный поиск  Timing: " + objT.Result().ToString());
            objT.StartTime();
            stpWatch.Start();
            SearchBinary(a, n);
            stpWatch.Stop();
            objT.StopTime();
            Console.WriteLine("Простой поиск StopWatch: " + stpWatch.Elapsed.ToString());
            Console.WriteLine("Бинарный поиск Timing: " + objT.Result().ToString());




            static int SimpleSearch(int[] a, int x)
            {

                int i = 0;
                while (i < a.Length && a[i] != x)
                    i++;
                if (i < a.Length)
                    return i;
                else
                    return -1;
            }
            static int SearchBinary(int[] a, int x)
            {
                int middle, left = 0, right = a.Length - 1;
                do
                {
                    middle = (left + right) / 2;
                    if (x > a[middle])
                        left = middle + 1;
                    else
                        right = middle - 1;
                }
                while ((a[middle] != x) && (left <= right));
                if (a[middle] == x)
                    return middle;
                else
                    return -1;
            }
        }
        internal class Timing
        {
            TimeSpan duration;
            TimeSpan[] a;
            public Timing()
            {
                duration = new TimeSpan(0);
                a = new TimeSpan[Process.GetCurrentProcess().
                Threads.Count];
            }
            public void StartTime()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                for (int i = 0; i < a.Length; i++)
                    a[i] = Process.GetCurrentProcess().Threads[i].
                     UserProcessorTime;
            }
            public void StopTime()
            {
                TimeSpan tmp;
                for (int i = 0; i < a.Length; i++)
                {
                    tmp = Process.GetCurrentProcess().Threads[i].
                    UserProcessorTime.Subtract(a[i]);
                    if (tmp > TimeSpan.Zero)
                        duration = tmp;
                }
            }
            public TimeSpan Result()
            {
                return duration;
            }
        }
    }
}
