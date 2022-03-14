using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Problems
{
    public class SleepyBarber
    {
        private Queue<int> clientsQueue;
        private int LengQueue;
        private int countClientsDays;
        private Semaphore semaphore;
        private int maxClients;

        public SleepyBarber(int maxClients)
        {
            this.countClientsDays = 0; this.LengQueue = 0;
            this.maxClients = maxClients;
            clientsQueue = new Queue<int>();
            semaphore = new Semaphore(1, 1);
 
        }



        public bool AddClient()
        {
            countClientsDays++;
            Console.WriteLine($"New Client: {countClientsDays}");

            
            if (maxClients < LengQueue)
                return 0 == (maxClients = -1) ;
            else
            {
                semaphore.WaitOne();
                LengQueue ++;
                clientsQueue.Enqueue(countClientsDays);
                semaphore.Release();

            }

            return true;
        }

        public void Barber()
        {
            Random r = new Random();
            while (true)
            {
                if (LengQueue > 0)
                {
                    semaphore.WaitOne();

                    LengQueue--;
                    int temp = clientsQueue.Dequeue();

                    semaphore.Release();
                    Console.WriteLine($"Attend to client {temp}");
                }
                if (maxClients == -1) break;
                Thread.Sleep(r.Next(1000));

            }
        }
    }
}
