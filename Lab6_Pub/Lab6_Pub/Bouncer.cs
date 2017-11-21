using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab6_Pub
{
    class Bouncer
    {
        private Action<string> Callback;
        private Action<Patron> CallbackPatron;
        Random rnd = new Random();
        Stopwatch stopwatch = new Stopwatch();
        private int BouncerSpeed = 1;
        bool TestBouncer = true;

        public bool IsWorking { get; set; }

        public event Action ClosingTime;

        List<string> NameList = new List<string>()
        {
        "Liam",
        "Mason",
        "Jacob",
        "William",
        "Ethan",
        "James",
        "Alexander",
        "Michael",
        "Benjamin",
        "Elijah"
        };

        public void BouncerWork(Action<string>Callback, Action<Patron>CallbackPatron, int BarOpenBouncer)
        {
            IsWorking = true;
            Task.Run(() =>
            {
                this.Callback = Callback;
                this.CallbackPatron = CallbackPatron;
                stopwatch.Start();
                while (stopwatch.Elapsed < TimeSpan.FromSeconds(BarOpenBouncer))
                {
                    Thread.Sleep(rnd.Next(3000 / BouncerSpeed, 10000 / BouncerSpeed));
                    string patronName = NameList[rnd.Next(NameList.Count)];
                    CallbackPatron(new Patron(patronName));
                    Callback($"{patronName} entered the bar.");
                }
                stopwatch.Stop();
                ClosingTime();
                Callback("The bouncer goes home!");
            });
        }
    }
}
