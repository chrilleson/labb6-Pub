using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Concurrent;
using System.Diagnostics;


namespace Lab6_Pub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Queues for the Patron
        ConcurrentQueue<string> uiPatronCountQueue = new ConcurrentQueue<string>();
        ConcurrentQueue<Patron> QueuePatron = new ConcurrentQueue<Patron>();
        ConcurrentQueue<Patron> QueueBartender = new ConcurrentQueue<Patron>();

        //Queues for the glasses
        ConcurrentStack<Glass> cleanGlassStack = new ConcurrentStack<Glass>();
        ConcurrentStack<Glass> dirtyGlassStack = new ConcurrentStack<Glass>();

        //Queues for the chairs
        ConcurrentStack<Chair> EmptyChairStack = new ConcurrentStack<Chair>();

        //Objects for our "agents"
        Bouncer bouncer = new Bouncer();
        Bartender bartender = new Bartender();
        Waitress waitress = new Waitress();
        
        //Variables for the diffrent test cases
        private int OpenBar = 120;
        private int BarOpenBouncer = 120;
        private int Glasses = 8;
        private int Chairs = 9;
        private int waitressPickingGlassesTime = 15000;
        private int waitressCleaningGlassesTime = 10000;
        private int speed = 1;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            bouncer.ClosingTime += waitress.StopServing;
            bouncer.ClosingTime += bartender.StopServing;

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            GlassStack();
            ChairStack();

            //Timer to show in the UI
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            bouncer.BouncerWork(PatronUpdateList, AddPatronQueue, BarOpenBouncer);
            bartender.BartenderWork(QueuePatron, QueueBartender, BartenderUpdateList, PatronUpdateList, cleanGlassStack,dirtyGlassStack,bouncer.IsWorking,EmptyChairStack, uiPatronCountQueue);
            waitress.Work(WaitressUpdateList, dirtyGlassStack, cleanGlassStack, QueuePatron, bouncer.IsWorking, waitressCleaningGlassesTime, waitressPickingGlassesTime, Glasses);
        }
        //Event handler for the on screen timer
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (OpenBar > 0)
            {
                OpenBar--;
                lblTime.Content = $"Time before the bar closes: {OpenBar}";
            }
            else
            {
                lblTime.Content = $"Time before the bar closes: 0";
            }
        }

        //Updateing the Patron Listbox
        private void PatronUpdateList(string info)
        {
            Dispatcher.Invoke(() =>
            {
                listBoxPatron.Items.Insert(0, info);

            });
        }
        //Update the bartender listbox
        private void BartenderUpdateList(string info)
        {
            Dispatcher.Invoke(() =>
            {
                listBoxBartender.Items.Insert(0, info);
                lblGlasses.Content = $"Glasses on the shelf: {cleanGlassStack.Count()}. ({Glasses} total).";
                lblChairs.Content = $"Empty seats int the bar: {EmptyChairStack.Count()}. ({Chairs} total).";
            });
        }
        //Update the waitress listbox
        private void WaitressUpdateList(string info)
        {
            Dispatcher.Invoke(() =>
            {
                listBoxWaitress.Items.Insert(0, info);
                lblGlasses.Content = $"Glasses on the shelf: {cleanGlassStack.Count()}. ({Glasses} total).";
            });
        }

        //Function that adds a Patron to the bar
        private void AddPatronQueue(Patron p)
        {
            QueuePatron.Enqueue(p);
            uiPatronCountQueue.Enqueue(p.Name);
        }
        //Creates glasses and adds them to the concurrentstack
        private void GlassStack()
        {
            for (int i = 0; i < Glasses; i++)
            {
                cleanGlassStack.Push(new Glass());
                Console.WriteLine("Added a glass to the stack.");
            }
        }
        //Creates chairs and adds them to the concurrentstack
        private void ChairStack()
        {
            for(int i = 0; i < Chairs; i++)
            {
                EmptyChairStack.Push(new Chair());
                Console.WriteLine("Added a chair to the stack.");
            }
        }
    }
}
