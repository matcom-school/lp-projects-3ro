using System;
using System.Threading;
using System.Collections.Generic;
using Problems;

namespace Tester
{
    static class TesterPhilosopher
    {
        public static void Run()
        {
            Philosopher.Fork[] forkes = new Philosopher.Fork[5] { 
                new Philosopher.Fork(1), new Philosopher.Fork(2), 
                new Philosopher.Fork(3), new Philosopher.Fork(4), 
                new Philosopher.Fork(5) };

            List<Philosopher> philosophers = new List<Philosopher>();

            philosophers.Add(new Philosopher(forkes[0], forkes[1], "Philosopher 1"));
            philosophers.Add(new Philosopher(forkes[1], forkes[2], "Philosopher 2"));
            philosophers.Add(new Philosopher(forkes[2], forkes[3], "Philosopher 3"));
            philosophers.Add(new Philosopher(forkes[3], forkes[4], "Philosopher 4"));
            philosophers.Add(new Philosopher(forkes[4], forkes[0], "Philosopher 5"));

            List<Thread> threads = new List<Thread>();

            foreach (var philo in philosophers)
                threads.Add(new Thread(philo.TryEat));

            foreach (var item in threads)
                item.Start();

            foreach (var item in threads)
                item.Join();
        }
    }
}