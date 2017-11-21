using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        ConcurrentQueue<string> uiPatronCountQueue = new ConcurrentQueue<string>();
        ConcurrentQueue<Patron> QueuePatron = new ConcurrentQueue<Patron>();

        Bouncer bouncer = new Bouncer();
        Bartender bartender = new Bartender();
        Waitress waitress = new Waitress();

        private int OpenBar = 120;
        private int BarOpenBouncer = 120;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;

            bouncer.BouncerWork(PatronUpdateList, AddPatronQueue, BarOpenBouncer);
        }

        private void PatronUpdateList(string info)
        {
            Dispatcher.Invoke(() =>
            {
                listBoxPatron.Items.Insert(0, info);

            });
        }

        private void AddPatronQueue(Patron p)
        {
            QueuePatron.Enqueue(p);
            uiPatronCountQueue.Enqueue(p.Name);
        }
    }
}
