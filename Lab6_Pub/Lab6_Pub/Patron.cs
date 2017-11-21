using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Lab6_Pub
{
    class Patron
    {
        public string Name { get; set; }
        Queue<string> NameQueue = new Queue<string>();
        Queue<string> temporaryQueue = new Queue<string>();

        private int PatronDrinkTimeMin = 10000;
        private int PatronDrinkTimeMax = 20000;

        public Patron(string name)
        {
            name = Name;
            NameQueue.Enqueue(Name);
        }

        private Action<string> Callback;
        private ConcurrentStack<Chair> EmptyChairStack;
        private ConcurrentStack<Glass> DirtyGlassStack;
        private ConcurrentQueue<Patron> PatronQueue;
        public string PatronDrinking { get; set; }
        Random rnd = new Random();

        //Function that makes the Patron sit down and drink, then leaves the bar
        public void Sit(Action<string> Callback, ConcurrentStack<Chair> EmptyChairStack, ConcurrentStack<Glass> DirtyGlassStack, ConcurrentQueue<Patron>PatronQueue, ConcurrentQueue<string> uiPatronCountDeQueue, int speed)
        {
            this.Callback = Callback;
            this.EmptyChairStack = EmptyChairStack;
            this.DirtyGlassStack = DirtyGlassStack;
            this.PatronQueue = PatronQueue;

            Task.Run(() =>
            {
                temporaryQueue.Enqueue(PatronQueue.FirstOrDefault().Name);
                PatronDrinking = temporaryQueue.First();
                temporaryQueue.Dequeue();
                PatronQueue.TryDequeue(out Patron p);

                while (EmptyChairStack.IsEmpty)
                {
                    Callback($"{PatronDrinking} is trying to find a seat.");
                    Thread.Sleep(1000 / speed);
                }
                EmptyChairStack.TryPop(out Chair c);
                Thread.Sleep(4000 / speed);
                Callback($"{PatronDrinking} takes a seat.");
                Thread.Sleep(rnd.Next(PatronDrinkTimeMin / speed, PatronDrinkTimeMax / speed));
                uiPatronCountDeQueue.TryDequeue(out string s);
                EmptyChairStack.Push(new Chair());
                DirtyGlassStack.Push(new Glass());
                Callback($"{PatronDrinking} is done drinking and leaves the bar.");
            });
        }
    }
}
