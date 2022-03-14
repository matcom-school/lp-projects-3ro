using System;
using System.Threading;


namespace Primitives
{
    public class CountdownEvent
    {
        Semaphore InnerSemaphore;
        Semaphore MultiSemaphore;
        int waiting;
        public int InitialCount {get; private set;}
        public int CurrentCount {get; private set;}
        public bool IsSet {get { return 0 == CurrentCount; } } 

        public CountdownEvent(int init) => Reset(init);
        public void Reset(int init = -1)
        {
            CurrentCount = InitialCount = init == -1 ? InitialCount : init;
            waiting = 0;
            InnerSemaphore = new Semaphore(1,1);
            MultiSemaphore = new Semaphore(0, int.MaxValue);
        }
        public void AddCount(int count)
        {
            InnerSemaphore.WaitOne();
            CurrentCount += count;
            InnerSemaphore.Release();
        }
        public void Signal(int count = 1)
        {
            InnerSemaphore.WaitOne();
            CurrentCount = CurrentCount > count ? CurrentCount - count : 0;
            
            if(CurrentCount == 0)
            {
                MultiSemaphore.Release(waiting);
                waiting = 0;
            }
            InnerSemaphore.Release();
        }
        public void Wait()
        {   
            InnerSemaphore.WaitOne();

            if (CurrentCount > 0)
            {
                waiting ++;
                InnerSemaphore.Release();
                MultiSemaphore.WaitOne();
            }
            else InnerSemaphore.Release();
        }
    }
}
