using System;
using System.Collections.Generic;
using System.Threading;

namespace Primitives
{
    public class Monitor
    {
        //objectsOnDemand-monitor
        static Dictionary<object, Monitor> ObjectMonitors = new Dictionary<object, Monitor>();
        static Semaphore StaticSemaphore = new Semaphore(1, 1);
        
        Semaphore InnerSemaphore;
        Monitor() => InnerSemaphore = new Semaphore(1,1);

        void Enter() => InnerSemaphore.WaitOne();
        void Exit() => InnerSemaphore.Release();

        public static void Enter(object obj)
        {
            StaticSemaphore.WaitOne();
            if (!ObjectMonitors.ContainsKey(obj))
                ObjectMonitors[obj] = new Monitor();
            StaticSemaphore.Release();
            ObjectMonitors[obj].Enter();

        }
        public static void Exit(object obj)
        {
            StaticSemaphore.WaitOne();
            if (!ObjectMonitors.ContainsKey(obj))
                throw new InvalidOperationException();
            StaticSemaphore.Release();
            ObjectMonitors[obj].Exit();

        }





    }
}