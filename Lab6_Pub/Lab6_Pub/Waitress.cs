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
        public bool BarIsOpen { get; set; }
        private int waitressSpeed = 1;

        public void Work(Action<string> callback, ConcurrentStack<Glass> dirtyGlassStack, ConcurrentStack<Glass> cleanGlassStack, bool bouncerIsWorking, ConcurrentQueue<Patron> patronQueue, int waitressWashingSec, int waitressPickingGlassesSec, int glasses)
        {
            this.Callback = callback;
            this.DirtyGlassStack = dirtyGlassStack;
            this.CleanGlassStack = cleanGlassStack;
            this.BarIsOpen = bouncerIsWorking;
            this.PatronQueue = patronQueue;

            Task.Run(() =>
            {
                while (BarIsOpen)
                {
                    while(CleanGlassStack.Count() != glasses)
                    {
                        if (!DirtyGlassStack.IsEmpty)
                        {
                            Callback("The waitress picks up dirty glasses from the table");
                            Thread.Sleep(waitressWashingSec / waitressSpeed);
                            Callback("The waitress is washing glasses");
                            Thread.Sleep(waitressPickingGlassesSec / waitressSpeed);
                            Callback("The waitress places the clean glasses back on the shelf");
                            for (int i = 0; i < DirtyGlassStack.Count(); i++)
                            {
                                DirtyGlassStack.TryPop(out Glass g);
                                CleanGlassStack.Push(new Glass());
                            }
                        }
                    }
                }
                callback("The waitress goes home.");
            });
        }
        public void StopServing()
        {
            BarIsOpen = false;
        }

        public void ChangeSpeed(int speed)
        {
            this.waitressSpeed = speed;
        }
    }
}
