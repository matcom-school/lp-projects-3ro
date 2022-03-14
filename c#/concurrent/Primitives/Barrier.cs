using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;


namespace Primitives
{
    public class Barrier
    {
        public int ParticipantsCount { get; private set; }
        public int ParticipantsRemaining { get; private set; }
        public int CurrentPhaseNumber { get; private set; }
        private Semaphore Semaphore;
        private Semaphore InnerSemaphore;
        private Action<Barrier> action;
        private int deltaParticipant;

        public Barrier(int participants, Action<Barrier> action = null)
        {
            ParticipantsCount = participants; 
            ParticipantsRemaining = 0;
            CurrentPhaseNumber = 0;
            InnerSemaphore = new Semaphore(1, 1);
            Semaphore = null;
            this.action = action;
        }

        private void ClosePhase() 
        {
            CurrentPhaseNumber++;
            Semaphore.Release(ParticipantsCount - 1);  

            ParticipantsRemaining = 0;
            Semaphore = null;
            InnerSemaphore.Release();
        }

        public void SignalAndWait()
        {
            InnerSemaphore.WaitOne();
            if (Semaphore == null)
                Semaphore = new Semaphore(0, ParticipantsCount - 1);
            
            ParticipantsRemaining++;

            if (ParticipantsRemaining == ParticipantsCount)
            {
                if (action != null)
                    try { action(this); }
                    catch (Exception e)
                    {
                        ClosePhase();
                        throw e;
                    }
                ClosePhase();
            }
            else
            {
                InnerSemaphore.Release();
                Semaphore.WaitOne();                    
            }
        }


        public int AddParticipants(int count = 1)
        {
            InnerSemaphore.WaitOne();
            ParticipantsCount += count;
            int result = CurrentPhaseNumber + 1;
            InnerSemaphore.Release();
            return result;
        }

        public int RemoveParticipant(int count = 1)
        {
            InnerSemaphore.WaitOne();
            if (ParticipantsCount <  count )
                throw new InvalidOperationException("Barrier has no " + count + " participant to remove");
            ParticipantsCount -= count;
            int nextPhase = CurrentPhaseNumber + 1;
            InnerSemaphore.Release();
            return nextPhase;
        }

    }
}
