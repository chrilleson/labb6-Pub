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
        /*
        private Action<string> Callback;
        private ConcurrentStack<Glass> DirtyGlassStack;
        private ConcurrentStack<Glass> CleanGlassStack;
        private ConcurrentQueue<Patron> PatronQueue;
        public bool BarIsOpen { get; set; }
        private int waiterSpeed = 1;

        public void Work(Action<string> Callback, ConcurrentStack<Glass> DirtyGlassStack, ConcurrentStack<Glass> CleanGlassStack,bool IsWorking, ConcurrentQueue<Patron>PatronQueue, int waitressWashingTime, int waitressPickingGlassesTime, int glasses)
        {
            this.Callback = Callback;
            this.DirtyGlassStack = DirtyGlassStack;
            this.CleanGlassStack = CleanGlassStack;
            this.BarIsOpen = IsWorking;
            this.PatronQueue = PatronQueue;
            Task.Run(() =>
            {
                while (BarIsOpen)
                {
                    while (CleanGlassStack.Count() != glasses)
                    {
                        if (!DirtyGlassStack.IsEmpty)
                        {
                            Callback("The waiter picks up dirty glasses from a table.");
                            Thread.Sleep(waitressWashingTime / waiterSpeed);
                            Callback("The waiter is washing glasses.");
                            Thread.Sleep(waitressPickingGlassesTime / waiterSpeed);
                            Callback("The waiter places the clean glasses back on the shelf.");
                            for (int i = 0; i < DirtyGlassStack.Count(); i++)
                            {
                                DirtyGlassStack.TryPop(out Glass g);
                                CleanGlassStack.Push(new Glass());
                            }
                        }
                    }
                }
                Callback("The waiter goes home.");
            });
        }
        public void StopServing()
        {
            BarIsOpen = false;
        }

        public void ChangeSpeed(int speed)
        {
            this.waiterSpeed = speed;
        }*/
    }
}
