using System;
using System.Threading;
using System.Collections.Generic;
using Problems;

namespace Tester
{
    static class TesterSlpeepyBarber
    {
       
        static public void Run()
        {   
            SleepyBarber sb = new SleepyBarber(3);
            Thread barber = new Thread(() => sb.Barber());
            barber.Start();
            Random r = new Random();
            while (true)
                if (r.Next(0,2) == 1)
                    if (!sb.AddClient()) break;
                    else Thread.Sleep(r.Next(1000));

            
            Console.WriteLine("Barbershop is full, close for today");
            barber.Join();
        }
    }
}