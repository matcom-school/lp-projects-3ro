using System;
using System.Threading;
using System.Threading.Tasks;
using Tester;


namespace concurrent
{
    class Program
    {
        static void Main(string[] args)
        {           
            Console.WriteLine("--------------Tester Barrier ---------------");
            Tester.TesterBarrier.Run();

            Console.WriteLine("\n--------------Tester Monitor ---------------");
            Tester.TesterMonitor.Run();

            Console.WriteLine("\n--------------Tester CountdownEvent ---------------");
            Tester.TesterCountdownEvent.Run().Wait();

            Console.WriteLine("\n--------------Philosopher ---------------");
            Tester.TesterPhilosopher.Run();

            Console.WriteLine("\n--------------Spleepy Barber ---------------");
            Tester.TesterSlpeepyBarber.Run();
        }
    }
}
