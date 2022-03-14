using System;
using System.Threading;

namespace Problems
{
    class Philosopher
    {
        static Random r = new Random();

        public class Fork
        {
            public int n;
            public Fork(int n) => this.n = n;
        }

        Fork left, right;
        string name;

        public Philosopher(Fork left, Fork right, string name)
        {
            this.left = left;
            this.right = right;
            this.name = name;
        }

        public void TryEat()
        {   
            bool _bool = true;
            while(_bool)
            {
                ThinkingAndWantsEat();
                if (!Monitor.TryEnter(left, r.Next(2000))) continue;
                try
                {
                    TakeFirstFork();
                    if (!Monitor.TryEnter(right, r.Next(2000))) continue;
                    try{ 
                        TakeSecondForkAndEating(); 
                        _bool = false;
                    }
                    finally
                    {
                        Monitor.Exit(right);
                        LeaveFork(right);
                    }
                }
                finally 
                {
                    Monitor.Exit(left);
                    LeaveFork(left);
                }

            }
        }


        #region Actions                   
        private void ThinkingAndWantsEat()
        {
            Console.WriteLine(name + " he is thinking \n");
            Thread.Sleep(r.Next(1000));
            Console.WriteLine(name + " he wants to eat \n");
        }
        private void TakeFirstFork()
        {
            Console.WriteLine(name + " has fork " + left.n + '\n');
            Thread.Sleep(r.Next(1000));
        }
        private void TakeSecondForkAndEating()
        {
            Console.WriteLine(name + " has fork " + right.n + '\n');
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(name + " is eating with the forks " + left.n + " and " + right.n + '\n');
            Console.ResetColor();
            Thread.Sleep(r.Next(1000));
        }
        private void LeaveFork(Fork fork) => Console.WriteLine(name + " leave the fork " + fork.n + " free" + '\n');
        #endregion
    }
}