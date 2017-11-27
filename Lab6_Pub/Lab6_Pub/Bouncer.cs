using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab6_Pub
{
    public class Bouncer
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

        //Bouncers normal work
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
        //Bus load work method
        public void BusNight(Action<string> Callback, Action<Patron> CallbackPatron, int BarOpenBouner)
        {
            IsWorking = true;
            Task.Run(() =>
            {
                this.CallbackPatron = CallbackPatron;
                this.Callback = Callback;
                stopwatch.Start();
                while (stopwatch.Elapsed < TimeSpan.FromSeconds(BarOpenBouner))
                {
                    if(stopwatch.Elapsed > TimeSpan.FromSeconds(20) && TestBouncer)
                    {
                        BusWithPatrons();
                        TestBouncer = false;
                    }
                    Thread.Sleep(rnd.Next(6000, 20000));
                    string PatronName = NameList[rnd.Next(NameList.Count())];
                    CallbackPatron(new Patron(PatronName));
                    Callback($"{PatronName} entered the bar.");
                }
                stopwatch.Stop();
                ClosingTime();
                Callback($"The Bartender goes home.");
            });
        }
        private void BusWithPatrons()
        {
            int Bus = 0;
            while (Bus < 15)
            {
                string PatronName = NameList[rnd.Next(NameList.Count())];
                CallbackPatron(new Patron(PatronName));
                Callback($"{PatronName} entred the bar.");
                Bus++;
            }
        }
        //Couples Night work method
        public void CouplesWork(Action<string> Callback, Action<Patron> CallbackPatron, int BarOpenBouncer)
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
                    CouplesNight();
                }
                stopwatch.Stop();
                ClosingTime();
                Callback("The bouncer goes home!");
            });
        }
        private void CouplesNight()
        {
                string PatronName1 = NameList[rnd.Next(NameList.Count())];
                string PatronName2 = NameList[rnd.Next(NameList.Count())];
                CallbackPatron(new Patron(PatronName1));
                CallbackPatron(new Patron(PatronName2));
                Callback($"{PatronName1} entred the bar with {PatronName2}.");
        }
    }
}
