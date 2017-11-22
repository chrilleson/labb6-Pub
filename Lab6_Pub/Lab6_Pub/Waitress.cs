using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace Lab6_Pub
{
    public class Waitress
    {
        private Action<string> Callback;
        private ConcurrentStack<Glass> DirtyGlassStack;
        private ConcurrentStack<Glass> CleanGlassStack;
        private ConcurrentQueue<Patron> PatronQueue;
        public bool IsWorking { get; set; }
        private int WaitressSpeed = 1;

        public void Work(Action<string> callback, ConcurrentStack<Glass> dirtyGlassStack, ConcurrentStack<Glass> cleanGlassStack, ConcurrentQueue<Patron> patronQueue, bool isWorking, int waitressCleanTime, int waitressPickingTime, int glasses)
        {
            this.Callback = callback;
            this.DirtyGlassStack = dirtyGlassStack;
            this.CleanGlassStack = cleanGlassStack;
            this.PatronQueue = patronQueue;
            this.IsWorking = isWorking;

            Task.Run(() =>
            {
                while (IsWorking)
                {
                    while (CleanGlassStack.Count() != glasses)
                    {
                        if (!DirtyGlassStack.IsEmpty)
                        {
                            Callback("The waitress is picking up dirty glasses.");
                            Thread.Sleep(waitressCleanTime / WaitressSpeed);
                            Callback("The waitress is washing the dirty glasses.");
                            Thread.Sleep(waitressPickingTime / WaitressSpeed);
                            Callback("The waitress is placing the glasses on the shelf.");
                            for (int i = 0; i < DirtyGlassStack.Count(); i++)
                            {
                                DirtyGlassStack.TryPop(out Glass g);
                                CleanGlassStack.Push(new Glass());
                            }
                        }
                    }
                }
                Callback("The waitress goes home.");
            });
        }
        public void StopServing()
        {
            IsWorking = false;
        }
    }
}
